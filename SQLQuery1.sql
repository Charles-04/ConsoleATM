USE ATMDatabase;
GO

INSERT INTO AccountUser(accountNumber,accountBalance,userName,pin)
VALUES (1023837237,30000,'Henry Ugo','9027'),(1073857237,80000,'John Udo','9577');

select * from AccountUser;