CREATE Table PlacedAds
(
	placedad_id			int				PRIMARY KEY IDENTITY(1,1),
	admatch_id			int				REFERENCES AdMatches(admatch_id),
	trigger_id			int				REFERENCES Triggers(trigger_id),
	uploadedad_id		int				REFERENCES UploadedAds(uploadedad_id)	NOT NULL,	
	owner_type			varchar(100)	NOT NULL,	
	scanned_datetime	datetime,			
	scanned_store_id	int				REFERENCES Stores(store_id),
	placement_datetime	datetime		NOT NULL	
)
