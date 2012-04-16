CREATE TABLE LogCodes
(
	logcode_id			int		PRIMARY KEY,	
	[description]		varchar(200)	NOT NULL,
)

Create Table FaultCodes
(
	faultcode_id		int		PRIMARY KEY,	
	[description]		varchar(200)	NOT NULL,
	display_message		varchar(200),
)


CREATE TABLE LogEntries
(
	logentry_id			int			PRIMARY KEY IDENTITY(1,1),	
	logcode_id			int			REFERENCES LogCodes(logcode_id) NOT NULL,	
	campaign_id			int			REFERENCES Campaigns(campaign_id),
	adgroup_id			int			REFERENCES AdGroups(adgroup_id),
	admatch_id			int			REFERENCES AdMatches(admatch_id),
	permission_id		int			REFERENCES [Permissions](permission_id),
	uploadedad_id		int			REFERENCES UploadedAds(uploadedad_id),
	company_id			int			REFERENCES Companies(company_id),	
	store_id			int			REFERENCES Stores(store_id),
	admin_id			int			REFERENCES Admins(admin_id),
	customer_id			int			REFERENCES Customers(customer_id),				
	owner_id			int			REFERENCES Admins(admin_id) NOT NULL,
	[description]		varchar(8000)	NOT NULL,
	ip_address			varchar(50),	
	creation_datetime		datetime	NOT NULL	
)


Create TABLE Faults
(
	fault_id			int		PRIMARY KEY IDENTITY(1,1),	
	faultcode_id		int		REFERENCES	FaultCodes(faultcode_id) NOT NULL,
	logentry_id			int		REFERENCES	LogEntries(logentry_id),	
	requested_page		varchar(200)	,
	[description]		varchar(8000)	NOT NULL,	
	creation_datetime	datetime	NOT NULL	
)


