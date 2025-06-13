--because these logging information will be stored in azure blob storage instead of database
Use master
go
CREATE DATABASE iLoveIbadahDB
GO
USE iLoveIbadahDB
GO
CREATE TABLE Blob_File
(
    id INT IDENTITY(1,1) PRIMARY KEY,      -- Primary key for each blob record
    uri NVARCHAR(300) NOT NULL,        -- URI of the blob (maximum length of URI is 2083 characters but it is too long, recommanded 500 but also to long, 250 minimum but maybe to short, so 300!)
    full_name NVARCHAR(255) NOT NULL,
    extension NVARCHAR(100) NOT NULL,
    size INT NOT NULL,          -- Size of the blob in bytes (optional)
	created_by INT NULL, -- well because profile picutre type and user account have their file id as nullable this should't pose an issue to have it here as required. but suppose in order to create a new user a profile picture need to be passed and that profile picture needs to reference the blob file, then this is circular dependency, so i can't create blob file cause i need to know who created it, but can't create user account because i need to pass his profile picture. so just let nullable for now to revieuw for later.

	--adding foreign key constraint at end because useraccount table doensn't exist when this is executed...
	--CONSTRAINT FK_Blob_File_created_by FOREIGN KEY (created_by) REFERENCES User_Account (id) ON DELETE CASCADE, 
	CONSTRAINT UQ_Blob_File_uri UNIQUE (uri)
);
GO
CREATE TABLE
   Profile_Picture_Type (
      id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
      Blob_File_id INT NOT NULL, -- it takes to much space but i allow this instead of just link to image storing because restricted to limited amount of image to not deal with censoring because it is a religious app it must be heavily heavily heavily censorized (leaderboard system!) and i can't use gravatar service for exemple because they and all other services that i know don't censor the way it islamicly must!
      --created_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format Automatically sets to the current date
      created_by INT NULL, -- So user can create his own personal profile picture types just for him depending on business rule (like paid user can do so because of manual censoring work to validate ? like brining islamic belief certain body parts should be covered this is more strict than in most belief systems.)
      --last_modified_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format
      --last_modified_by BIGINT NOT NULL,

	  CONSTRAINT FK_Profile_Picture_Type_Blob_File_id FOREIGN KEY (Blob_File_id) REFERENCES Blob_File (id) ON DELETE CASCADE,
	  CONSTRAINT UQ_Profile_Picture_Type_Blob_File_id UNIQUE (Blob_File_id)
      --CONSTRAINT FK_Profile_Picture_Type_created_by FOREIGN KEY (created_by) REFERENCES User_Account (id),
      --CONSTRAINT FK_Profile_Picture_Type_last_modified_by FOREIGN KEY (last_modified_by) REFERENCES User_Account (id),
      -- CONSTRAINT UQ_Profile_Picture_Type_base64_code UNIQUE (base64_code) -- Ensures a unique record per Salah type. Couldn't do it cause cannot index on image type to long characters, chatgpt propose me to hash the base64_code and that I could put unique constraint on hash but I will just skip unique constraint!
   );
GO
CREATE TABLE
   User_Account (
      id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
      full_name NVARCHAR (25) NOT NULL, -- isn't unique! yes!!! no more "this username is already used" error!!!! well yes... in unique_id still the case
	  unique_id NVARCHAR (35) NOT NULL, --like in twitter @username so the unique username
	  normalized_unique_id NVARCHAR (35) NULL, --for aspnetcore identity!
      email NVARCHAR (100) NOT NULL, -- Ensuring email is unique
	  normalized_email NVARCHAR (100) NULL, --for aspnetcore identity!
      Profile_Picture_Type_id INT DEFAULT 1 NULL, -- Base64 encryption for Project!
      password_hash NVARCHAR (255) NULL,
      email_confirmed BIT DEFAULT 0 NULL,
      current_location NVARCHAR (75) NULL, -- stores city only privacy compliant. longest city name is 58 characters 75 should be enough for all cases even futures ones normaly.
	  --current_longitude DECIMAL(11, 8) NULL,
	  --current_latitude DECIMAL(10, 8) NULL,
      total_warnings INT DEFAULT 0 NULL, -- user cheat and has impossible score so leaderboard messed up, username is non sharia complient, and group name (later future) forbidden interactions. in terms of condition and policy
	  is_permanently_banned BIT DEFAULT 0 NULL, -- after 2 warnings perma ban, Only 1 type of banning for now, perma ban for ease of development, implementation of temporary ban is maybe later addon
      --created_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format Automatically sets to the current date
      --last_modified_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format
      --last_modified_by BIGINT NULL, --do i add TRIGGER?????
      --CONSTRAINT FK_User_Account_last_modified_by FOREIGN KEY (last_modified_by) REFERENCES User_Account (id),
	  concurrency_stamp NVARCHAR (36) NULL, --for aspnetcore identity!
	  security_stamp NVARCHAR (36) NULL, --for aspnetcore identity!

	  CONSTRAINT FK_User_Account_Profile_Picture_Type_id FOREIGN KEY (Profile_Picture_Type_id) REFERENCES Profile_Picture_Type (id) ON DELETE SET NULL,
      CONSTRAINT UQ_User_Account_email UNIQUE (email),
	  CONSTRAINT UQ_User_Account_normalized_email UNIQUE (normalized_email),
      CONSTRAINT UQ_User_Account_email_password_hash UNIQUE (email, password_hash),
      --CONSTRAINT UQ_User_Account_email_oauth_id UNIQUE (email, oauth_id),
	  CONSTRAINT UQ_User_Account_unique_id UNIQUE (unique_id),
      --CONSTRAINT CK_User_Account_password_hash_oauth_id CHECK (password_hash IS NOT NULL OR oauth_id IS NOT NULL) -- At least one must be non-NULL
   );

GO

CREATE TABLE
	User_Account_External_Login (
		id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
		User_Account_id INT NOT NULL,
		oauth_provider NVARCHAR (25) NOT NULL,
        oauth_key NVARCHAR (255) NOT NULL,
		oauth_full_name NVARCHAR (25) NULL,

		CONSTRAINT FK_User_Account_External_Login_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account (id) ON DELETE CASCADE,
		--CONSTRAINT UQ_User_Account_External_Login_User_Account_id UNIQUE (User_Account_id),
		CONSTRAINT UQ_User_Account_External_Login_oauth_key UNIQUE (oauth_key)
	);

GO

CREATE TABLE
	User_Account_Authentication_Token (
		id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
		User_Account_id INT NOT NULL,
		login_provider NVARCHAR (25) NOT NULL,
        unique_id NVARCHAR (25) NOT NULL,
		jwt_value NVARCHAR (800) NOT NULL,
		jwt_value_hash CHAR(64) NULL,

		CONSTRAINT FK_User_Account_Authentication_Token_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account (id) ON DELETE CASCADE,
		--CONSTRAINT UQ_User_Account_Authentication_Token_User_Account_id UNIQUE (User_Account_id), --not applicable in our context, what if user both register with built in authentication and oauth! or 2 accounts or same account... not for now implementation!
		CONSTRAINT UQ_User_Account_Authentication_Token_jwt_value_hash UNIQUE (jwt_value_hash)
	);

GO

CREATE TABLE
   User_Account_Claim_Type_Mapping (
      id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
      User_Account_id INT NOT NULL,
      claim_type NVARCHAR(50) NOT NULL, --no claimtype table!
	  claim_value NVARCHAR(50) NOT NULL, --no claimtype table!
      --created_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format Automatically sets to the current date
      --created_by BIGINT NOT NULL, -- So user can create his own personal dhirk types just for him
      --last_modified_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format
      --last_modified_by BIGINT NOT NULL,
      --CONSTRAINT FK_Role_Type_Permission_Type_created_by FOREIGN KEY (created_by) REFERENCES User_Account (id),
      --CONSTRAINT FK_Role_Type_Permission_Type_last_modified_by FOREIGN KEY (last_modified_by) REFERENCES User_Account (id),
      CONSTRAINT FK_User_Account_Claim_Type_Mapping_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account (id) ON DELETE CASCADE,
      --CONSTRAINT FK_Role_Type_Claim_Type_Mapping_Claim_Type_id FOREIGN KEY (Claim_Type_id) REFERENCES Claim_Type (id) ON DELETE CASCADE,
      CONSTRAINT UQ_User_Account_Claim_Type_Mapping_User_Account_id_claim_type_claim_value UNIQUE (User_Account_id, claim_type, claim_value)
   );

GO

CREATE TABLE
   Role_Type (
      id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
      full_name NVARCHAR (50) NOT NULL, -- Name of the role (e.g., 'Regular User', 'Premium User') or for later functionality of groups of people, gorup id + admin
      details NVARCHAR (255) NULL, -- Description of the role
      --created_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format Automatically sets to the current date
      --created_by BIGINT NOT NULL, -- So user can create his own personal dhirk types just for him
      --last_modified_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format
      --last_modified_by BIGINT NOT NULL,
      --CONSTRAINT FK_Role_Type_created_by FOREIGN KEY (created_by) REFERENCES User_Account (id),
      --CONSTRAINT FK_Role_Type_last_modified_by FOREIGN KEY (last_modified_by) REFERENCES User_Account (id),
      CONSTRAINT UQ_Role_Type_full_name UNIQUE (full_name)
   );

GO

--CREATE TABLE
--   Claim_Type (
--      id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
--      full_name NVARCHAR (50) NOT NULL, -- Name of the role (e.g., 'Regular User', 'Premium User') or for later functionality of groups of people, gorup id + admin
--      details NVARCHAR (255) NULL, -- Description of the role
--	  data_type NVARCHAR(50) NULL, -- wheter bool, string, int collection<list blablabla> and so on. so i can validate userclaim value or roleclaim value...
--      --created_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format Automatically sets to the current date
--      --created_by BIGINT NOT NULL, -- So user can create his own personal dhirk types just for him
--      --last_modified_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format
--      --last_modified_by BIGINT NOT NULL,
--      --CONSTRAINT FK_Role_Type_created_by FOREIGN KEY (created_by) REFERENCES User_Account (id),
--      --CONSTRAINT FK_Role_Type_last_modified_by FOREIGN KEY (last_modified_by) REFERENCES User_Account (id),
--      CONSTRAINT UQ_Claim_Type_full_name UNIQUE (full_name)
--   );

--CREATE TABLE
--   Permission_Type (
--      id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
--      full_name NVARCHAR (50) NOT NULL, -- Name of the role (e.g., 'Regular User', 'Premium User')
--      details NVARCHAR (255) NULL, -- Description of the role
--      --created_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format Automatically sets to the current date
--      --created_by BIGINT NOT NULL, -- So user can create his own personal dhirk types just for him
--      --last_modified_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format
--      --last_modified_by BIGINT NOT NULL,
--      --CONSTRAINT FK_Permission_Type_created_by FOREIGN KEY (created_by) REFERENCES User_Account (id),
--      --CONSTRAINT FK_Permission_Type_last_modified_by FOREIGN KEY (last_modified_by) REFERENCES User_Account (id),
--      CONSTRAINT UQ_Permission_Type_full_name UNIQUE (full_name)
--   );

GO

CREATE TABLE
   Role_Type_Claim_Type_Mapping (
      id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
      Role_Type_id INT NOT NULL,
      claim_type NVARCHAR(50) NOT NULL, --no claimtype table!
	  claim_value NVARCHAR(50) NOT NULL, --no claimtype table!
      --created_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format Automatically sets to the current date
      --created_by BIGINT NOT NULL, -- So user can create his own personal dhirk types just for him
      --last_modified_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format
      --last_modified_by BIGINT NOT NULL,
      --CONSTRAINT FK_Role_Type_Permission_Type_created_by FOREIGN KEY (created_by) REFERENCES User_Account (id),
      --CONSTRAINT FK_Role_Type_Permission_Type_last_modified_by FOREIGN KEY (last_modified_by) REFERENCES User_Account (id),
      CONSTRAINT FK_Role_Type_Claim_Type_Mapping_Role_Type_id FOREIGN KEY (Role_Type_id) REFERENCES Role_Type (id) ON DELETE CASCADE,
      --CONSTRAINT FK_Role_Type_Claim_Type_Mapping_Claim_Type_id FOREIGN KEY (Claim_Type_id) REFERENCES Claim_Type (id) ON DELETE CASCADE,
      CONSTRAINT UQ_Role_Type_Claim_Type_Mapping_Role_Type_id_claim_type_claim_value UNIQUE (Role_Type_id, claim_type, claim_value)
   );

--CREATE TABLE
--   Role_Type_Permission_Type_Mapping (
--      id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
--      Role_Type_id INT NOT NULL,
--      Permission_Type_id INT NOT NULL,
--      --created_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format Automatically sets to the current date
--      --created_by BIGINT NOT NULL, -- So user can create his own personal dhirk types just for him
--      --last_modified_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format
--      --last_modified_by BIGINT NOT NULL,
--      --CONSTRAINT FK_Role_Type_Permission_Type_created_by FOREIGN KEY (created_by) REFERENCES User_Account (id),
--      --CONSTRAINT FK_Role_Type_Permission_Type_last_modified_by FOREIGN KEY (last_modified_by) REFERENCES User_Account (id),
--      CONSTRAINT FK_Role_Type_Permission_Type_Role_Type_id FOREIGN KEY (Role_Type_id) REFERENCES Role_Type (id) ON DELETE CASCADE,
--      CONSTRAINT FK_Role_Type_Permission_Type_Permission_id FOREIGN KEY (Permission_Type_id) REFERENCES Permission_Type (id) ON DELETE CASCADE,
--      CONSTRAINT UQ_Role_Type_Permission_Type_Role_Type_id_Permission_id UNIQUE (Role_Type_id, Permission_Type_id)
--   );

GO
CREATE TABLE
   User_Account_Role_Type_Mapping (
      id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
      User_Account_id INT NOT NULL,
      Role_Type_id INT DEFAULT 2 NOT NULL, --1 is admin 2 is normal user
      --created_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format Automatically sets to the current date
      --created_by BIGINT NOT NULL, -- So user can create his own personal dhirk types just for him
      --last_modified_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format
      --last_modified_by BIGINT NOT NULL,
      --CONSTRAINT FK_User_Account_Role_Type_created_by FOREIGN KEY (created_by) REFERENCES User_Account (id),
      --CONSTRAINT FK_User_Account_Role_Type_last_modified_by FOREIGN KEY (last_modified_by) REFERENCES User_Account (id),
      CONSTRAINT FK_User_Account_Role_Type_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account (id) ON DELETE CASCADE,
      CONSTRAINT FK_User_Account_Role_Type_Role_Type_id FOREIGN KEY (Role_Type_id) REFERENCES Role_Type (id) ON DELETE CASCADE,
      CONSTRAINT UQ_User_Account_Role_Type_User_Account_id_Role_Type_id UNIQUE (User_Account_id, Role_Type_id)
   );

   -- Because I realized user account ban type is a 1 on many relationship, I Will just add a BanType property to the UserAccount class

--GO
--CREATE TABLE
--   Ban_Type (
--      id INT PRIMARY KEY IDENTITY (1, 1),
--      total_warnings INT NOT NULL,
--      ban_duration INT NULL, -- Duration in days (e.g., 7 for one week)
--      is_permanent BIT DEFAULT 0 NULL,
--      --created_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format Automatically sets to the current date
--      --created_by BIGINT NOT NULL, -- So user can create his own personal dhirk types just for him
--      --last_modified_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format
--      --last_modified_by BIGINT NOT NULL,
--      --CONSTRAINT FK_Ban_Type_created_by FOREIGN KEY (created_by) REFERENCES User_Account (id),
--      --CONSTRAINT FK_Ban_Type_last_modified_by FOREIGN KEY (last_modified_by) REFERENCES User_Account (id),
--      CONSTRAINT UQ_Ban_Type_total_warnings UNIQUE (total_warnings),
--      CONSTRAINT CK_Ban_Type_ban_duration_is_permanent CHECK (
--         ban_duration IS NOT NULL
--         OR is_permanent IS NOT NULL
--      ) -- At least one must be non-NULL
--   );

--GO
--CREATE TABLE
--   User_Account_Ban_Type_Mapping (
--      id INT PRIMARY KEY IDENTITY (1, 1),
--      User_Account_id BIGINT NOT NULL,
--      Ban_Type_id INT NOT NULL,
--      --banned_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format Automatically sets to the current date
--      --banned_by BIGINT NOT NULL,
--      --last_modified_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format
--      --last_modified_by BIGINT NOT NULL,
--      --CONSTRAINT FK_User_Account_Ban_Type_banned_by FOREIGN KEY (banned_by) REFERENCES User_Account (id),
--      --CONSTRAINT FK_User_Account_Ban_Type_last_modified_by FOREIGN KEY (last_modified_by) REFERENCES User_Account (id),
--      CONSTRAINT FK_User_Account_Ban_Type_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account (id),
--      CONSTRAINT FK_User_Account_Ban_Type_Ban_Type_id FOREIGN KEY (Ban_Type_id) REFERENCES Ban_Type (id),
--      CONSTRAINT UQ_User_Account_Ban_Type_User_Account_id_Ban_Type_id UNIQUE (User_Account_id, Ban_Type_id)
--   );

--GO
--CREATE TABLE
--   Warning_Type (
--      id BIGINT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
--      full_name NVARCHAR (255) NOT NULL, --Allahu Akbar, Subhan Allah, Alhamdullillah, Astaghfirullah etc...
--      details NVARCHAR (255) NULL, -- Description of the warning
--      --created_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format Automatically sets to the current date
--      --created_by BIGINT NOT NULL, -- So user can create his own personal dhirk types just for him
--      --last_modified_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format
--      --last_modified_by BIGINT NOT NULL,
--      --CONSTRAINT FK_Warning_Type_created_by FOREIGN KEY (created_by) REFERENCES User_Account (id),
--      --CONSTRAINT FK_Warning_Type_last_modified_by FOREIGN KEY (last_modified_by) REFERENCES User_Account (id),
--      CONSTRAINT UQ_Warning_Type_full_name UNIQUE (full_name) -- Ensures a unique record per dhikr type.
--   );

GO
CREATE TABLE
   Dhikr_Type (
      id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
      full_name NVARCHAR (255) NOT NULL, --Allahu Akbar, Subhan Allah, Alhamdullillah, Astaghfirullah etc...
	  arabic_full_name NVARCHAR (255) NOT NULL, 
      --created_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format Automatically sets to the current date
      created_by INT NOT NULL, -- So user can create his own personal dhirk types just for him
      --last_modified_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format
      --last_modified_by BIGINT NOT NULL,
      --CONSTRAINT FK_Dhikr_Type_created_by FOREIGN KEY (created_by) REFERENCES User_Account (id),
      --CONSTRAINT FK_Dhikr_Type_last_modified_by FOREIGN KEY (last_modified_by) REFERENCES User_Account (id),
      CONSTRAINT UQ_Dhikr_Type_created_by_full_name UNIQUE (created_by, full_name) -- Ensures a unique record per dhikr type.
	  --CONSTRAINT UQ_Dhikr_Type_created_by_arabic_full_name UNIQUE (created_by, arabic_full_name)
   );
   --ALTER TABLE Dhikr_Type
   --ADD arabic_full_name NVARCHAR(255) NOT NULL DEFAULT '';

GO
CREATE TABLE
   Salah_Type (
      id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
      full_name NVARCHAR (255) NOT NULL, -- fajr, sobh, dohr, maghreb, isha, witr etc...
      --created_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format Automatically sets to the current date
      created_by INT NOT NULL, -- So user can create his own personal dhirk types just for him
      --last_modified_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format
      --last_modified_by BIGINT NOT NULL,
      --CONSTRAINT FK_Salah_Type_created_by FOREIGN KEY (created_by) REFERENCES User_Account (id),
      --CONSTRAINT FK_Salah_Type_last_modified_by FOREIGN KEY (last_modified_by) REFERENCES User_Account (id),
      CONSTRAINT UQ_Salah_Type_full_name UNIQUE (full_name) -- Ensures a unique record per Salah type.
   );
GO
CREATE TABLE
   User_Dhikr_Activity (
      id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
      User_Account_id INT NOT NULL, -- Foreign key to Users table
      Dhikr_Type_id INT NOT NULL, -- Foreign key to Dhikr table
      performed_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NULL, -- The date in YYYY-MM-DD format in which the activity occurred
      last_performed_at DATETIME DEFAULT GETDATE () NULL,
      total_performed INT DEFAULT 1 NULL, -- Default count to 0 for new records
      CONSTRAINT FK_User_Dhikr_Activity_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account (id) ON DELETE CASCADE,
      CONSTRAINT FK_User_Dhikr_Activity_Dhikr_Type_id FOREIGN KEY (Dhikr_Type_id) REFERENCES Dhikr_Type (id) ON DELETE CASCADE,
      CONSTRAINT UQ_User_Dhikr_Activity_User_Account_id_Dhikr_Type_id_performed_at UNIQUE (User_Account_id, Dhikr_Type_id, performed_on) -- Ensures a unique record per user per day per dhikr
   );

GO
CREATE TABLE
   User_Salah_Activity (
      id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
      User_Account_id INT NOT NULL, -- Foreign key to Users table
      Salah_Type_id INT NOT NULL, -- Foreign key to Dhikr table
      tracked_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NULL, -- The date in YYYY-MM-DD format in which the activity occurred
      punctuality_percentage DECIMAL(4, 2) DEFAULT 0 NOT NULL, -- The percentage of punctuality in prayer time (e.g., 98.50)
      CONSTRAINT FK_User_Salah_Activity_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account (id) ON DELETE CASCADE,
      CONSTRAINT FK_User_Salah_Activity_Salah_Type_id FOREIGN KEY (Salah_Type_id) REFERENCES Salah_Type (id) ON DELETE CASCADE,
      CONSTRAINT UQ_User_Salah_Activity_User_Account_id_Salah_Type_id_tracked_on UNIQUE (User_Account_id, Salah_Type_id, tracked_on) -- Ensures a unique record per user per day
   );

GO
CREATE TABLE
   User_Salah_Overview (
      id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
      User_Account_id INT NOT NULL, -- Foreign key to Users table
      --average_punctuality_percentage DECIMAL(5,2) DEFAULT 0 NOT NULL, -- Average punctuality percentage
      total_tracked INT DEFAULT 0 NOT NULL, -- Total of salah records taken into account for the average punctuality percentage
      last_tracked_at DATETIME NULL,
      CONSTRAINT FK_User_Salah_Overview_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account (id) ON DELETE CASCADE
   );

GO
CREATE TABLE
   User_Dhikr_Overview (
      id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
      User_Account_id INT NOT NULL, -- Foreign key to Users table
      total_performed INT DEFAULT 0 NOT NULL, -- Total dhikr performed by the user
      last_performed_at DATETIME NULL, -- Timestamp for when the overview was last updated
      CONSTRAINT FK_User_Dhikr_Overview_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account (id) ON DELETE CASCADE
   );

GO
CREATE TABLE Blog (
   id INT PRIMARY KEY IDENTITY (1, 1),
   title NVARCHAR(255) NOT NULL,
   slug NVARCHAR(255) NOT NULL,
   Blob_File_id INT NOT NULL, -- The Thumbnail for the blog!
   content NVARCHAR(MAX) NOT NULL,
   total_views INT DEFAULT 0 NOT NULL,
   created_at DATETIME DEFAULT GETDATE() NOT NULL,

   CONSTRAINT FK_Blog_Blob_File_id FOREIGN KEY (Blob_File_id) REFERENCES Blob_File (id) ON DELETE SET NULL,
   CONSTRAINT UQ_Blog_slug UNIQUE (slug)
);
GO

-- =============================================
-- Comment Table
-- =============================================
CREATE TABLE Comment (
   id INT PRIMARY KEY IDENTITY (1, 1),
   Blog_id INT NOT NULL,
   User_Account_id INT NOT NULL,
   content NVARCHAR(MAX) NOT NULL,
   written_at DATETIME DEFAULT GETDATE() NOT NULL,
   last_updated_at DATETIME NOT NULL DEFAULT GETDATE(),
   Comment_id INT NULL, --Parent Id, so is this a reply to another comment or a reply to the blog (top level)?
   CONSTRAINT FK_Comment_Blog_id FOREIGN KEY (Blog_id) REFERENCES Blog(id) ON DELETE CASCADE,
   CONSTRAINT FK_Comment_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account(id) ON DELETE CASCADE,
   --CONSTRAINT FK_Comment_Comment_id FOREIGN KEY (Comment_id) REFERENCES Comment(id) ON DELETE CASCADE : (Couldn't put on delete cascade, sql server doesn't support it says cycle or multiple cascade path error so on delete no action I did, and i can in c# code do a delete on the comments if i want)
   CONSTRAINT FK_Comment_Comment_id FOREIGN KEY (Comment_id) REFERENCES Comment(id) ON DELETE NO ACTION
);
GO

-- =============================================
-- Blog_Like Table
-- =============================================
CREATE TABLE Blog_Like (
   --id INT PRIMARY KEY IDENTITY(1, 1),
   Blog_id INT NOT NULL,
   User_Account_id INT NOT NULL,
   CONSTRAINT FK_Blog_Like_Blog_id FOREIGN KEY (Blog_id) REFERENCES Blog(id) ON DELETE CASCADE,
   CONSTRAINT FK_Blog_Like_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account(id) ON DELETE CASCADE,
   CONSTRAINT UQ_Blog_Like_Blog_id_User_Account_id UNIQUE (Blog_id, User_Account_id)
);
GO

-- =============================================
-- Comment_Like Table
-- =============================================
CREATE TABLE Comment_Like (
   --id INT PRIMARY KEY IDENTITY(1, 1),
   Comment_id INT NOT NULL,
   User_Account_id INT NOT NULL,
   CONSTRAINT FK_Comment_Like_Comment_id FOREIGN KEY (Comment_id) REFERENCES Comment(id) ON DELETE CASCADE,
   CONSTRAINT FK_Comment_Like_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account(id) ON DELETE NO ACTION,
   CONSTRAINT UQ_Comment_Like_Comment_id_User_Account_id UNIQUE (Comment_id, User_Account_id)
);

GO

CREATE TABLE 
	Category (
		id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
		full_name NVARCHAR (50) NOT NULL,

		CONSTRAINT UQ_Category_full_name UNIQUE (full_name)
	);

GO

CREATE TABLE 
	Tag (
		id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
		full_name NVARCHAR (50) NOT NULL, 
		--Category_id INT NULL,

		--CONSTRAINT FK_Tag_Category_id FOREIGN KEY (Category_id) REFERENCES Category (id) ON DELETE SET NULL,
		CONSTRAINT UQ_Tag_full_name UNIQUE (full_name)
	);

GO

CREATE TABLE Blog_Category_Mapping (
   --id INT PRIMARY KEY IDENTITY(1, 1),
   Blog_id INT NOT NULL,
   Category_id INT NOT NULL,

   CONSTRAINT FK_Blog_Category_Mapping_Blog_id FOREIGN KEY (Blog_id) REFERENCES Blog(id) ON DELETE CASCADE,
   CONSTRAINT FK_Blog_Category_Mapping_Category_id FOREIGN KEY (Category_id) REFERENCES Category(id) ON DELETE CASCADE,
   CONSTRAINT UQ_Blog_Category_Mapping_Blog_id_Category_id UNIQUE (Blog_id, Category_id)
);

GO

CREATE TABLE Blog_Tag_Mapping (
   --id INT PRIMARY KEY IDENTITY(1, 1),
   Blog_id INT NOT NULL,
   Tag_id INT NOT NULL,

   CONSTRAINT FK_Blog_Tag_Mapping_Blog_id FOREIGN KEY (Blog_id) REFERENCES Blog(id) ON DELETE CASCADE,
   CONSTRAINT FK_Blog_Tag_Mapping_Tag_id FOREIGN KEY (Tag_id) REFERENCES Tag(id) ON DELETE CASCADE,
   CONSTRAINT UQ_Blog_Tag_Mapping_Blog_id_Tag_id UNIQUE (Blog_id, Tag_id)
);

GO

--GO
--CREATE TABLE
--   Action_Type (
--      id INT PRIMARY KEY IDENTITY (1, 1), -- Auto-incrementing primary key
--      full_name NVARCHAR (50) NOT NULL, -- Name of the role (e.g., 'Regular User', 'Premium User')
--	  command_type NVARCHAR (50) NOT NULL, -- ENUM Insert, Update, Delete! exemple: ban => insert, unban => update, archive => delete
--      created_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format Automatically sets to the current date
--      created_by BIGINT NOT NULL, -- So user can create his own personal dhirk types just for him
--      last_modified_on DATE DEFAULT CONVERT(VARCHAR(10), GETDATE (), 120) NOT NULL, -- The date in YYYY-MM-DD format
--      last_modified_by BIGINT NOT NULL,
--      CONSTRAINT FK_Permission_Type_created_by FOREIGN KEY (created_by) REFERENCES User_Account (id),
--      CONSTRAINT FK_Permission_Type_last_modified_by FOREIGN KEY (last_modified_by) REFERENCES User_Account (id),
--      CONSTRAINT UQ_Permission_Type_full_name UNIQUE (full_name)
--   );
--GO
--CREATE TABLE Action_Log (
--    id INT PRIMARY KEY IDENTITY,
--    table_type_id INT NOT NULL,       -- OBJECT_ID of the database table for wich i am keeping logging information!!!
--	table_type_record_id INT NOT NULL,
--    Action_Type_id NVARCHAR(100) NULL,        -- Specific business action: Ban, Unban, perform, create, archive, etc.
--    created_at DATETIME DEFAULT GETDATE(),   -- When the action log was taken/occured
--    User_Account_id BIGINT NOT NULL,                        -- User who performed the action

--	CONSTRAINT FK_Action_Log_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account (id),
--    CONSTRAINT FK_Action_Log_Type_modified_by FOREIGN KEY (last_modified_by) REFERENCES User_Account (id),
--    CONSTRAINT UQ_Action_Log_ UNIQUE (full_name)
--);
--GO
--CREATE TABLE User_Ibadah_Overview (
--    id BIGINT PRIMARY KEY IDENTITY(1,1), -- Auto-incrementing primary key
--    User_Account_id BIGINT NOT NULL, -- Foreign key to Users table
--	Dhikr_Type_id BIGINT NOT NULL, -- Foreign key to Users table
--    total_dhikr_performed BIGINT DEFAULT 0 NOT NULL, -- Total dhikr performed by the user
----    average_punctuality_percentage DECIMAL(5,2) DEFAULT 0 NOT NULL, -- Average punctuality percentage
----    last_updated DATETIME DEFAULT GETDATE() NOT NULL, -- Timestamp for when the overview was last updated
--    CONSTRAINT FK_User_Ibadah_Overview_Dhikr_Type_id FOREIGN KEY (Dhikr_Type_id) REFERENCES Dhikr_Type(id) ON DELETE CASCADE,
--    CONSTRAINT FK_User_Ibadah_Overview_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account(id) ON DELETE CASCADE
--);
--GO
--CREATE TABLE User_Salah_Overview (
--    id BIGINT PRIMARY KEY IDENTITY(1,1), -- Auto-incrementing primary key
--    User_Account_id BIGINT NOT NULL, -- Foreign key to Users table
--    average_punctuality_percentage DECIMAL(5,2) DEFAULT 0 NOT NULL, -- Average punctuality percentage
--    last_updated DATETIME DEFAULT GETDATE() NOT NULL, -- Timestamp for when the overview was last updated
--    CONSTRAINT FK_User_Ibadah_Overview_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account(id) ON DELETE CASCADE
--);
--GO
--CREATE TABLE User_Dhikr_Overview (
--    id BIGINT PRIMARY KEY IDENTITY(1,1), -- Auto-incrementing primary key
--    User_Account_id BIGINT NOT NULL, -- Foreign key to Users table
--    total_dhikr_performed BIGINT DEFAULT 0 NOT NULL, -- Total dhikr performed by the user
--    average_punctuality_percentage DECIMAL(5,2) DEFAULT 0 NOT NULL, -- Average punctuality percentage
--    last_updated DATETIME DEFAULT GETDATE() NOT NULL, -- Timestamp for when the overview was last updated
--    CONSTRAINT FK_User_Ibadah_Overview_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account(id) ON DELETE CASCADE
--);
--GO
--CREATE TABLE User_Ibadah_Overview (
--    id BIGINT PRIMARY KEY IDENTITY(1,1), -- Auto-incrementing primary key
--    User_Account_id BIGINT NOT NULL, -- Foreign key to Users table
--	Dhikr_Type_id BIGINT NOT NULL, -- Foreign key to Users table
--    total_dhikr_performed BIGINT DEFAULT 0 NOT NULL, -- Total dhikr performed by the user
----    average_punctuality_percentage DECIMAL(5,2) DEFAULT 0 NOT NULL, -- Average punctuality percentage
----    last_updated DATETIME DEFAULT GETDATE() NOT NULL, -- Timestamp for when the overview was last updated
--    CONSTRAINT FK_User_Ibadah_Overview_Dhikr_Type_id FOREIGN KEY (Dhikr_Type_id) REFERENCES Dhikr_Type(id) ON DELETE CASCADE,
--    CONSTRAINT FK_User_Ibadah_Overview_User_Account_id FOREIGN KEY (User_Account_id) REFERENCES User_Account(id) ON DELETE CASCADE
--);
--Chatgpt4o mini
--GO
--CREATE TRIGGER Trigger_Insert_Dhikr_Overview on User_Dhikr_Activity
--	AFTER UPDATE
--AS
--BEGIN
--	SET NOCOUNT ON;
--	DECLARE @User_Account_id BIGINT
--	DECLARE @total_performed BIGINT
--    SELECT @User_Account_id = INSERTED.User_Account_id
--	FROM INERTED
--	IF UPDATE(total_performed)
--		SET total_performed += 1
--	WHERE User_Account_id = NEW.User_Account_id;
--END;
--GO
--CREATE TRIGGER Trigger_Update_Dhikr_Overview on User_Dhikr_Activity
--AFTER UPDATE
--FOR EACH ROW
--BEGIN
--    UPDATE User_Dhikr_Overview
--    SET total_performed += 1
--    WHERE User_Account_id = NEW.User_Account_id;
--END;
--TRIGGER FOR TOTAL TRACKED IN USER SALAH OVERVIEW BY CHATGPT

 
CREATE TRIGGER Trigger_Update_User_Salah_Overview_total_tracked ON User_Salah_Activity AFTER INSERT AS BEGIN
UPDATE uso
SET
   uso.total_tracked = uso.total_tracked + 1, -- Increment by exactly 1 per business rule
   uso.last_tracked_at = GETDATE () -- Update the last_performed_at timestamp
FROM
   User_Salah_Overview uso
   INNER JOIN inserted i ON uso.User_Account_id = i.User_Account_id END;

GO
--CREATE TRIGGER Trigger_Increment_User_Salah_Overview_total_tracked
--ON User_Salah_Activity
--AFTER INSERT
--AS
--BEGIN
--    -- Update the total_tracked column in User_Salah_Overview for the associated User_Account_id
--    UPDATE uso
--    SET uso.total_tracked = uso.total_tracked + 1,
--        uso.last_tracked_at = GETDATE()
--    FROM User_Salah_Overview uso
--    INNER JOIN inserted i ON uso.User_Account_id = i.User_Account_id;
--END;
--GO
--TRIGGER FOR TOTAL PERFORMED IN USER DHIKR OVERVIEW BY CHATGPT
CREATE TRIGGER Trigger_Update_User_Dhikr_Overview_total_performed ON User_Dhikr_Activity AFTER
UPDATE AS BEGIN
-- Check if total_performed was updated
IF (
   UPDATE (total_performed)
) BEGIN
UPDATE udo
SET
   udo.total_performed = udo.total_performed + 1, -- Increment by exactly 1 per business rule
   udo.last_performed_at = GETDATE () -- Update the last_performed_at timestamp
FROM
   User_Dhikr_Overview udo
   INNER JOIN inserted i ON udo.User_Account_id = i.User_Account_id
   INNER JOIN deleted d ON d.User_Account_id = i.User_Account_id
WHERE
   i.total_performed = d.total_performed + 1;

-- Ensure the increment is exactly 1
END END;

GO
CREATE TRIGGER Trigger_Increment_User_Dhikr_Overview_total_performed --when dhikr activity record is made it means there was an activity for space eficiency and many other reasons avoiding creating unnessary data in database!
ON User_Dhikr_Activity AFTER INSERT AS BEGIN
-- Increment total_performed by 1 in User_Dhikr_Overview for the associated User_Account_id
UPDATE udo
SET
   udo.total_performed = udo.total_performed + 1,
   udo.last_performed_at = GETDATE ()
FROM
   User_Dhikr_Overview udo
   INNER JOIN inserted i ON udo.User_Account_id = i.User_Account_id;

END;

--TRIGGER FOR CCREATING USER DHIKR OVERVIEW RECORD FOR NEW USER BY CHATGPT
GO 
CREATE TRIGGER Trigger_Create_User_Dhikr_Overview ON User_Account AFTER INSERT AS BEGIN
-- Insert a record into User_Dhikr_Overview for each new User_Account created
INSERT INTO
   User_Dhikr_Overview (User_Account_id)
SELECT
   id
FROM
   inserted;

END;

--TRIGGER FOR CCREATING USER SALAH OVERVIEW RECORD FOR NEW USER BY CHATGPT
GO 
CREATE TRIGGER Trigger_Create_User_Salah_Overview ON User_Account AFTER INSERT AS BEGIN
-- Insert a record into User_Dhikr_Overview for each new User_Account created
INSERT INTO
   User_Salah_Overview (User_Account_id)
SELECT
   id
FROM
   inserted;

END;

GO

CREATE TRIGGER Trigger_Create_User_Account_Role_Type_Mapping ON User_Account AFTER INSERT AS BEGIN
-- Insert a record into User_Dhikr_Overview for each new User_Account created
INSERT INTO
   User_Account_Role_Type_Mapping (User_Account_id, Role_Type_id)
SELECT
   id, 2
FROM
   inserted;

END;

GO
CREATE TRIGGER Trigger_Update_User_Account_is_permanently_banned 
ON User_Account 
AFTER UPDATE 
AS
BEGIN
    -- Check if total_warnings was updated
    IF UPDATE(total_warnings)
    BEGIN
        -- Update is_permanently_banned to true for relevant rows
        UPDATE ua
        SET ua.is_permanently_banned = 1 -- BIT datatype, 1 = true
        FROM User_Account ua
        WHERE ua.id IN (SELECT i.id FROM inserted i WHERE i.total_warnings = 2);
    END
END;

GO

CREATE TRIGGER Trigger_Update_Comment ON Comment AFTER UPDATE AS BEGIN
    -- Update last_updated_at for all updated rows
    UPDATE Comment
    SET last_updated_at = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;

GO

--CREATE TRIGGER Trigger_Update_User_Account_is_permanently_banned 
--ON User_Account 
--AFTER UPDATE 
--AS
--BEGIN
--    -- Check if total_warnings was updated
--    IF UPDATE(total_warnings)
--    BEGIN
--        -- Update is_permanently_banned to true for relevant rows
--        UPDATE ua
--        SET ua.is_permanently_banned = 1 -- BIT datatype, 1 = true
--        FROM User_Account ua
--        INNER JOIN inserted i ON ua.id = i.id
--        WHERE i.total_warnings = 2;
--    END
--END;

--GO
--CREATE TRIGGER Trigger_Update_Salah_Day_Overview
--ON User_Salah_Activity
--AFTER INSERT, UPDATE
--AS
--BEGIN
--    -- Update the average punctuality percentage
--    UPDATE User_Salah_Day_Overview
--    SET average_punctuality_percentage = (
--        SELECT AVG(punctuality_percentage)
--        FROM User_Salah_Activity
--        WHERE User_Account_id = inserted.User_Account_id
--        AND performed_at = inserted.performed_at
--    )
--    FROM User_Salah_Day_Overview
--    WHERE User_Account_id = inserted.User_Account_id
--    AND performed_at = inserted.performed_at;
--    -- Insert a new record if it did not exist before
--    IF NOT EXISTS (
--        SELECT 1
--        FROM User_Salah_Day_Overview
--        WHERE User_Account_id = inserted.User_Account_id
--        AND performed_at = inserted.performed_at
--    )
--    BEGIN
--        INSERT INTO User_Salah_Day_Overview (User_Account_id, performed_at, average_punctuality_percentage)
--        SELECT User_Account_id, performed_at, AVG(punctuality_percentage)
--        FROM User_Salah_Activity
--        WHERE User_Account_id = inserted.User_Account_id
--        AND performed_at = inserted.performed_at
--        GROUP BY User_Account_id, performed_at;
--    END
--END;
--GEMINI
--CREATE TRIGGER Trigger_Update_Dhikr_Overview
--AFTER INSERT, UPDATE ON User_Dhikr_Activity
--FOR EACH ROW
--BEGIN
--    UPDATE User_Dhikr_Overview
--    SET total_performed += 1
--    WHERE User_Account_id = NEW.User_Account_id;
--END;
--GO
--CREATE TRIGGER Trigger_Update_Salah_Day_Overview
--AFTER INSERT, UPDATE ON User_Salah_Activity
--FOR EACH ROW
--BEGIN
--    DECLARE @userId INT = NEW.User_Account_id;
--    DECLARE @date DATE = NEW.performed_at;
--    WITH SalahPunctuality AS (
--        SELECT punctuality_percentage
--        FROM User_Salah_Activity
--        WHERE User_Account_id = @userId
--        AND performed_at = @date
--    )
--    UPDATE User_Salah_Day_Overview
--    SET average_punctuality_percentage = (
--        SELECT AVG(punctuality_percentage)
--        FROM SalahPunctuality
--    )
--    WHERE User_Account_id = @userId
--    AND performed_at = @date;
--    IF @@ROWCOUNT = 0
--    BEGIN
--        INSERT INTO User_Salah_Day_Overview (User_Account_id, performed_at, average_punctuality_percentage)
--        SELECT @userId, @date, AVG(punctuality_percentage)
--        FROM SalahPunctuality;
--    END;
--END;

ALTER TABLE Blob_File ADD CONSTRAINT FK_Blob_File_created_by FOREIGN KEY (created_by) REFERENCES User_Account (id) ON DELETE NO ACTION;
GO
--INSERT INTO table (column) value (x);
INSERT INTO Blob_File (uri, full_name, extension, size) values ('test', 'test', 'svg', 100)
GO
INSERT INTO Profile_Picture_Type (Blob_File_id) values (1)

GO
ALTER DATABASE iLoveIbadahDB
SET
   READ_WRITE 
GO
USE iLoveIbadahDB
GO
SET
   ANSI_NULLS ON
GO
SET
   QUOTED_IDENTIFIER ON 
GO