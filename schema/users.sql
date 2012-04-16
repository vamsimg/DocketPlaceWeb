CREATE TABLE Admins
(
	admin_id			int				PRIMARY KEY IDENTITY(1,1),
	first_name			varchar(100)	NOT NULL,
	last_name			varchar(100)	NOT NULL,
	email				varchar(100)	NOT NULL	UNIQUE,
	phone				varchar(100)	NOT NULL,
	mobile				varchar(100)	NOT NULL,
	password_hash		char(128)		NOT NULL,
	is_active			bit				NOT NULL,	
	creation_datetime	datetime		NOT NULL
)

CREATE TABLE Roles
(
	role_name			varchar(100)	PRIMARY KEY,
	notes				varchar(100)	NOT NULL
)

CREATE TABLE [Permissions]
(
	permission_id		int				PRIMARY KEY IDENTITY(1,1),
	role_name			varchar(100)	FOREIGN KEY REFERENCES Roles(role_name)	NOT NULL,
	admin_id			int				FOREIGN KEY REFERENCES Admins(admin_id)	NOT NULL,	
	company_id			int				FOREIGN KEY REFERENCES Companies(company_id)	NOT NULL,
	authoriser_id		int				FOREIGN KEY REFERENCES Admins(admin_id)	NOT NULL,
	company_position	varchar(100)	NOT NULL,	
	creation_datetime	datetime		NOT NULL,	
	CONSTRAINT perm_unique UNIQUE NONCLUSTERED (admin_id, company_id)
)



