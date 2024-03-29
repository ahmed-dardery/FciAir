use FciAir;
/*==============================================================*/
/* Table: Sample Data                                           */
/*==============================================================*/

delete from [dbo].[Tickets]
delete from [dbo].[Monitor]
delete from [dbo].[Customers]
delete from [dbo].[Flights]
delete from [dbo].[Aircrafts]
delete from [dbo].[Admins]

--password is hash value of '1234'
insert into [dbo].[Admins] values ('Belal','Hamdy','belalhamdy','81DC9BDB52D04DC20036DBD8313ED055')
insert into [dbo].[Admins] values ('ahmed','darder','drdr','81DC9BDB52D04DC20036DBD8313ED055')
insert into [dbo].[Admins] values ('abdo','okasha','oksha','81DC9BDB52D04DC20036DBD8313ED055')
insert into [dbo].[Admins] values ('adham','mamdouh','adham','81DC9BDB52D04DC20036DBD8313ED055')

insert into [dbo].[Aircrafts] values (1,40,'AAA','Samir','19981215',12500)
insert into [dbo].[Aircrafts] values (1,20,'BBB','Farah','19971111',10000)
insert into [dbo].[Aircrafts] values (2,35,'CCC','Merna','19951010',20000)
insert into [dbo].[Aircrafts] values (3,15,'DDD','Ezzat','19910205',17000)
insert into [dbo].[Aircrafts] values (4,10,'EEE','Ahmed','19800705',11000)
insert into [dbo].[Aircrafts] values (1,32,'AAA','Sayed','19910302',15000)
insert into [dbo].[Aircrafts] values (2,47,'DDD','Mahmoud','19900205',12000)

insert into [dbo].[Customers] values ('Mohamed','Mostafa','111','EG','19990412','streamer','81DC9BDB52D04DC20036DBD8313ED055')
insert into [dbo].[Customers] values ('Toqa','Khaled','222','EG','19990512','toqa','81DC9BDB52D04DC20036DBD8313ED055')
insert into [dbo].[Customers] values ('Rasha','Hamoda','333','EG','19730803','rasha','81DC9BDB52D04DC20036DBD8313ED055')
insert into [dbo].[Customers] values ('Malak','Mostafa','444','EG','20100307','malak','81DC9BDB52D04DC20036DBD8313ED055')
insert into [dbo].[Customers] values ('mariam','Mostafa','555','EG','20150412','mariam','81DC9BDB52D04DC20036DBD8313ED055')
insert into [dbo].[Customers] values ('Ahmed','mamdouh','666','DE','19940412','ahmed','81DC9BDB52D04DC20036DBD8313ED055')
insert into [dbo].[Customers] values ('seif','mamdouh','777','EG','19950712','seif','81DC9BDB52D04DC20036DBD8313ED055')
insert into [dbo].[Customers] values ('Anas','Hamdy','888','EG','19971012','anas','81DC9BDB52D04DC20036DBD8313ED055')

insert into [dbo].[Flights] values (1,'20190418 10:34:09 AM','20190418 11:34:09 AM',20,'Egypt','Germany')
insert into [dbo].[Flights] values (4,'20190518 10:34:09 AM','20190418 11:34:09 AM',5,'Cairo','Alex')
insert into [dbo].[Flights] values (5,'20190419 05:34:09 AM','20190419 10:34:09 AM',12,'Germany','France')
insert into [dbo].[Flights] values (2,'20190422 07:34:09 AM','20190422 11:34:09 AM',15,'Egypt','France')
insert into [dbo].[Flights] values (2,'20190423 10:34:09 PM','20190423 11:34:09 PM',17,'France','England')
insert into [dbo].[Flights] values (3,'20190425 10:34:09 AM','20190425 11:34:09 AM',25,'Egypt','Germany')
insert into [dbo].[Flights] values (6,'20190430 06:34:09 AM','20190430 07:34:09 AM',23,'Alex','Sharm El sheikh')
insert into [dbo].[Flights] values (7,'20190429 10:34:09 PM','20190429 11:34:09 PM',11,'Sharm El sheikh','Cairo')
insert into [dbo].[Flights] values (1,'20190618 10:34:09 AM','20190618 11:34:09 AM',20,'Egypt','Germany')
insert into [dbo].[Flights] values (4,'20190718 10:34:09 AM','20190718 11:34:09 AM',5,'Cairo','Alex')
insert into [dbo].[Flights] values (5,'20190919 05:34:09 AM','20190919 10:34:09 AM',12,'Germany','France')
insert into [dbo].[Flights] values (2,'20191022 07:34:09 AM','20191022 11:34:09 AM',15,'Egypt','France')
insert into [dbo].[Flights] values (2,'20190823 10:34:09 PM','20190823 11:34:09 PM',17,'France','England')
insert into [dbo].[Flights] values (3,'20190825 10:34:09 AM','20190825 11:34:09 AM',25,'Egypt','Germany')
insert into [dbo].[Flights] values (6,'20190730 06:34:09 AM','20190730 07:34:09 AM',23,'Alex','Sharm El sheikh')
insert into [dbo].[Flights] values (7,'20190629 10:34:09 PM','20190629 11:34:09 PM',11,'Sharm El sheikh','Cairo')

insert into [dbo].[Monitor] values (1,2)
insert into [dbo].[Monitor] values (2,1)
insert into [dbo].[Monitor] values (3,5)
insert into [dbo].[Monitor] values (2,6)
insert into [dbo].[Monitor] values (3,3)
insert into [dbo].[Monitor] values (1,6)
insert into [dbo].[Monitor] values (4,2)
insert into [dbo].[Monitor] values (4,7)

insert into [dbo].[Tickets] values (9,3,100,'child','first','20190419 07:34:09 AM')
insert into [dbo].[Tickets] values (9,8,100,'child','first','20190419 09:34:09 AM')
insert into [dbo].[Tickets] values (9,1,100,'child','first','20190419 11:34:09 AM')
insert into [dbo].[Tickets] values (8,6,200,'infant','economy','20190420 07:34:09 AM')
insert into [dbo].[Tickets] values (8,2,200,'infant','economy','20190420 11:34:09 AM')
insert into [dbo].[Tickets] values (5,3,50,'Adult','first','20190419 07:34:09 AM')
insert into [dbo].[Tickets] values (5,4,70,'Adult','first','20190419 09:34:09 AM')
insert into [dbo].[Tickets] values (9,6,100,'infant','economy','20190420 07:34:09 AM')
insert into [dbo].[Tickets] values (6,5,40,'child','second','20190419 07:34:09 AM')
insert into [dbo].[Tickets] values (4,4,100,'child','economy','20190419 07:34:09 AM')
insert into [dbo].[Tickets] values (1,2,10,'Adult','first','20190420 07:34:09 AM')