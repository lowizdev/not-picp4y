CREATE TABLE "users" (
	"id"	INTEGER NOT NULL,
	"name"	TEXT NOT NULL,
	"email"	TEXT NOT NULL UNIQUE,
	"cpf"	TEXT NOT NULL UNIQUE,
	"password"	TEXT NOT NULL,
	"usertype"	INTEGER NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);


CREATE TABLE "payments" (
	"paymentidentifier"	TEXT,
	"value"	NUMERIC NOT NULL,
	"payerid"	INTEGER NOT NULL,
	"payeeid"	INTEGER NOT NULL,
	"paymentstatus"	INTEGER NOT NULL
)


CREATE TABLE "wallets" (
	"id"	INTEGER NOT NULL,
	"userid"	INTEGER NOT NULL,
	"value"	NUMERIC NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);

INSERT INTO users (name, email, cpf, password, usertype) VALUES ("test1", "test1@email.com", "123", "123", 1);
INSERT INTO users (name, email, cpf, password, usertype) VALUES ("test2", "test2@email.com", "456", "456", 1);
INSERT INTO wallets (userid, value) VALUES (1, 5000);
INSERT INTO wallets (userid, value) VALUES (2, 5000);