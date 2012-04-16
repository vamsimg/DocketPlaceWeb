CREATE TABLE Campaigns
(
	campaign_id			int			PRIMARY KEY IDENTITY(1,1),
	company_id			int			REFERENCES Companies(company_id)	NOT NULL,
	creator_id			int			REFERENCES Admins(admin_id)			NOT NULL,
	title				varchar(100)	NOT NULL,
	notes				varchar(1000)	,
	budget				int			NOT NULL,
	is_active			bit			NOT NULL,
	is_archived			bit			NOT NULL,	
	start_datetime		datetime	NOT NULL,
	end_datetime		datetime	NOT NULL,
	creation_datetime	datetime	NOT NULL
)

CREATE TABLE AdGroups
(
	adgroup_id			int			PRIMARY KEY IDENTITY(1,1),
	campaign_id			int			REFERENCES Campaigns(campaign_id)	NOT NULL,
	creator_id			int			REFERENCES Admins(admin_id)			NOT NULL,
	title				varchar(100)	NOT NULL,
	notes				varchar(1000),	
	budget				int			NOT NULL,
	is_active			bit			NOT NULL,
	start_datetime		datetime	NOT NULL,
	end_datetime		datetime	NOT NULL,
	creation_datetime	datetime	NOT NULL	
)



CREATE TABLE AdMatches
(
	admatch_id			int			PRIMARY KEY IDENTITY(1,1),	
	store_id			int			REFERENCES Stores(store_id)		NOT NULL,
	adgroup_id			int			REFERENCES Adgroups(adgroup_id)	NOT NULL,
	notes				varchar(1000),	
	is_published		bit			NOT NULL,
	is_approved			bit,
	is_rejected			bit,	
	is_active			bit			NOT NULL,
	is_unique_barcode	bit			NOT NULL,				
	start_datetime		datetime	NOT NULL,
	end_datetime		datetime	NOT NULL,
	expiry_datetime		datetime	NOT NULL,
	creation_datetime	datetime	NOT NULL		
)

CREATE TABLE RequestedAds
(
	admatch_id			int REFERENCES AdMatches(admatch_id),
	uploadedad_id		int REFERENCES UploadedAds(uploadedad_id),
	num_wanted			int	NOT NULL,
	daily_quota			int NOT NULL,
	num_printed			int	NOT NULL,
	is_active			bit NOT NULL,
	PRIMARY KEY(admatch_id, uploadedad_id)
)