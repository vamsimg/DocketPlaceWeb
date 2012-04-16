CREATE Table OutgoingSMS
(
	outgoingSMS_id		int				PRIMARY KEY IDENTITY(1,1),
	admin_id			int				REFERENCES Admins(admin_id) NOT NULL, 
	billingitem_id		int				REFERENCES BillingItems(billingitem_id),	
	customerlist_id		int				REFERENCES CustomerLists(customerlist_id),
	message_text		varchar(1000)	NOT NULL,
	notes				varchar(1000),
	receipient_list		varchar(max)	NOT NULL,
	response_list		varchar(max)	NOT NULL,	
	broken_list			varchar(max)	NOT NULL,
	count				int				NOT NULL,
	verification_sms	varchar(6)		NOT NULL,
	unsubscribe_list	varchar(max),
	sent_datetime		datetime		NOT NULL,	
)		