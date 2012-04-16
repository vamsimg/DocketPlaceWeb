CREATE Table CustomerLists
(
	customerlist_id		int				PRIMARY KEY IDENTITY(1,1),
	admin_id			int				REFERENCES Admins(admin_id) NOT NULL, 
	company_id			int				REFERENCES CustomerLists(customerlist_id) NOT NULL,
	title				varchar(1000)	NOT NULL	,
	select_statement	varchar(max)	NOT NULL,
	notes				varchar(max)	,
	creation_datetime	datetime		NOT NULL,	
)		