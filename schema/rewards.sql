CREATE TABLE RewardSettings
(
	setting_id			int				PRIMARY KEY IDENTITY(1,1),
	company_id			int				FOREIGN KEY REFERENCES Companies(company_id) UNIQUE	NOT NULL,	
	expiry_days			int				NOT NULL,
	points_per_dollar	int				NOT NULL,
	points_threshold	int				NOT NULL,
	voucher_amount		money			NOT NULL,
	enable_vouchers		bit				NOT NULL	
)

CREATE TABLE Customers
(
	customer_id			int				PRIMARY KEY IDENTITY(1,1),
	email				varchar(100)	,
	email_broken		bit				NOT NULL default 0,		
	mobile				varchar(100)	,
	phone				varchar(100)	,
	title				varchar(10),
	sex					bit, 
	first_name			varchar(100),
	last_name			varchar(100),
	suburb				varchar(100),
	state				varchar(100),			
	postcode			varchar(10),
	password_hash		char(128)		NOT NULL,
	is_active			bit				NOT NULL,
	no_sms				bit				NOT NULL default 0,  	
	no_email			bit				NOT NULL default 0,  	
	verification_code	char(8)			NOT NULL,
	creation_datetime	datetime		NOT NULL	
)

CREATE Table Members
(
	customer_id			int				FOREIGN KEY REFERENCES Customers(customer_id)	NOT NULL,	
	company_id			int				FOREIGN KEY REFERENCES Companies(company_id)	NOT NULL,		
	store_id			int				FOREIGN KEY REFERENCES Stores(store_id)	NOT NULL,		
	local_customer_id	varchar(100)	,	
	local_barcode_id	varchar(100)	,		
	total_revenue		money,
	frequency			int,	
	reward_points		int				NOT NULL,
	grade				varchar(100)	,	
	creation_datetime	datetime		NOT NULL,
	PRIMARY KEY(customer_id, company_id)
)

Create Table Vouchers
(
	voucher_id			int				PRIMARY KEY IDENTITY(1,1),
	customer_id			int				FOREIGN KEY REFERENCES Customers(customer_id)	NOT NULL,	
	company_id			int				FOREIGN KEY REFERENCES Companies(company_id)	NOT NULL,		
	code				char(8)			NOT NULL,
	dollar_value		money			NOT NULL,
	creation_datetime	datetime		NOT NULL,
	expiry_datetime		datetime		NOT NULL,
	used_datetime		datetime			
)	

CREATE Table Dockets
(
	docket_id			int				PRIMARY KEY IDENTITY(1,1),
	local_id			int,				
	code				varchar(8)		NOT NULL,
	customer_id			int				FOREIGN KEY REFERENCES Customers(customer_id),			
	store_id			int				FOREIGN KEY REFERENCES Stores(store_id)	NOT NULL,			
	placedad_id			int				FOREIGN KEY REFERENCES PlacedAds(placedad_id),			
	total				money			NOT NULL,
	reward_points		int,
	raw_content			varchar(max),
	creation_datetime	datetime		NOT NULL,			
)

Create Table DocketItems
(	
	docketitem_id		int				PRIMARY KEY IDENTITY(1,1),
	docket_id			int				FOREIGN KEY REFERENCES Dockets(docket_id)	NOT NULL,				
	product_code		varchar(100),
	product_barcode		varchar(100),	
	description			varchar(200)	NOT NULL,
	department			varchar(100),		
	goods_cost				money			NOT NULL,
	unit_cost				money			NOT NULL,
	quantity			float			NOT NULL
)

Create Table PointsLog
(
	pointlog_id			int				PRIMARY KEY IDENTITY(1,1),
	customer_id			int				FOREIGN KEY REFERENCES Customers(customer_id)	NOT NULL,	
	company_id			int				FOREIGN KEY REFERENCES Companies(company_id)	NOT NULL,		
	reward_points		int				NOT NULL,
	description			varchar(1000)	NOT NULL,	
	creation_datetime	datetime		NOT NULL,			
	docket_id			int				FOREIGN KEY REFERENCES Dockets(docket_id),
	voucher_id			int				FOREIGN KEY REFERENCES Vouchers(voucher_id),
	admin_id			int				FOREIGN KEY REFERENCES Admins(admin_id)
)


