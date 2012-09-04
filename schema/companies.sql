CREATE Table Companies
(
	company_id			int				PRIMARY KEY IDENTITY(1,1),
	name				varchar(100)	NOT NULL,
	abn					varchar(20)		NOT NULL,
	contact_name		varchar(100)	NOT NULL,
	contact_email		varchar(100)	NOT NULL,
	address				varchar(100)	NOT NULL,
	suburb				varchar(100)	NOT NULL,
	state				varchar(20)		NOT NULL,
	postcode			varchar(20)		NOT NULL,
	phone				varchar(100)	NOT NULL,
	fax					varchar(100),
	mobile				varchar(100),
	technical_contact	varchar(1000)	NOT NULL,
	website				varchar(100),
	notes				varchar(8000),
	SMSCaller_id		varchar(11),	
	is_advertiser		bit				NOT NULL,
	is_retailer			bit				NOT NULL,
	is_active			bit				NOT NULL,
	are_receipts_stored bit				NOT NULL,		
	is_rewards			bit				NOT NULL,
	creation_datetime	datetime		NOT NULL,
	paidto_datetime		datetime	
	mailchimp_apikey	varchar(1000),
	mc_masterlist_id	varchar(100),
)

CREATE Table UploadedAds
(
	uploadedad_id		int				PRIMARY KEY IDENTITY(1,1),
	company_id			int				REFERENCES Companies(company_id) NOT NULL,
	title				varchar(100)	NOT NULL,
	notes				varchar(1000)	NOT NULL,
	data				varchar(max)	NOT NULL,
	footer				varchar(100)	NOT NULL,
	global_barcode		varchar(100),
	is_active			bit				NOT NULL,
	creation_datetime	datetime		NOT NULL
)

CREATE Table Stores
(
	store_id			int				PRIMARY KEY IDENTITY(1,1),	
	company_id			int				FOREIGN KEY REFERENCES Companies(company_id)	NOT NULL,
	default_uploadedad_id			int		FOREIGN KEY REFERENCES UploadedAds(uploadedad_id),
	password		char(8)		NOT NULL,
	store_contact		varchar(1000)	NOT NULL,
	address				varchar(100)	NOT NULL,
	suburb				varchar(100)	NOT NULL,
	state				char(3)			NOT NULL,
	postcode			char(4)			NOT NULL,
	num_printers		int				NOT NULL,
	avg_volume			int				NOT NULL,	
	creation_datetime	datetime		NOT NULL	
)


