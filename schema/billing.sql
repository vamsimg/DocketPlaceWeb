CREATE TABLE Invoices
(
	invoice_id			int				PRIMARY KEY IDENTITY(1,1),
	company_id			int				REFERENCES Companies(company_id) NOT NULL,
	terms				varchar(100)	NOT NULL,
	notes				varchar(2000)	NOT NULL,
	payment_method		varchar(2000),
	total_amount		money			NOT NULL,
	is_credit			bit				NOT NULL,
	is_paid				bit				NOT NULL,	
	start_datetime		datetime		NOT NULL,
	end_datetime		datetime		NOT NULL,
	creation_datetime	datetime		NOT NULL,
	paid_datetime		datetime			
)

CREATE Table BillingItems
(
	billingitem_id		int				PRIMARY KEY IDENTITY(1,1),
	invoice_id			int				REFERENCES Invoices(invoice_id),
	company_id			int				REFERENCES Companies(company_id) NOT NULL,	
	description			varchar(1000)	NOT NULL,
	quantity			int				NOT NULL,
	unit_cost			money			NOT NULL,
	total_amount		money			NOT NULL,
	is_credit			bit				NOT NULL,
	creation_datetime	datetime		NOT NULL				
)