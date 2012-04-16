Create Table Triggers
(
	trigger_id			int			PRIMARY KEY IDENTITY(1,1),		
	store_id			int			REFERENCES Stores(store_id) NOT NULL,	
	uploadedad_id		int			REFERENCES UploadedAds(uploadedad_id) NOT NULL,
	creator_id			int			REFERENCES Admins(admin_id) NOT NULL,
	priority			int			NOT NULL,
	header				 		varchar(1000),
	type				 		varchar(100) NOT NULL,
	value				 		varchar(1000) NOT NULL,
	notes						varchar(max) NOT NULL,
	is_active			bit 		NOT NULL,	
	creation_datetime		datetime	NOT NULL		
)