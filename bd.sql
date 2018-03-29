create database test;

	use test;

	exec ('create schema persons');

		create table persons.user_data
				(
					id_user		int						primary key identity,
					name 			varchar(45)		not null,
					email			varchar(45)		not null,
					password	varchar(45)		not null,
					removed		int 					not null deafult(0)
				);