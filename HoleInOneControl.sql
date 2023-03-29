USE hole_in_one_control;

CREATE TABLE user (
	id_user INT PRIMARY KEY AUTO_INCREMENT,
    user_name VARCHAR(50) NOT NULL,
    name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL
);

CREATE TABLE article (
	id_article INT PRIMARY KEY AUTO_INCREMENT,
    id_user INT,
    name_article VARCHAR(100) NOT NULL,
    brand VARCHAR(50),
    model VARCHAR(50),
    capacity int,
    color VARCHAR(50),
    type VARCHAR(50),
    material VARCHAR(50),
    description VARCHAR(500),
    FOREIGN KEY (id_user) REFERENCES user(id_user)
);

CREATE TABLE transaction
(
	id_transaction INT PRIMARY KEY AUTO_INCREMENT,
	id_user INT,
    date_hour datetime,
    type VARCHAR(50) NOT NULL,
    FOREIGN KEY (id_user) REFERENCES user(id_user)
);

CREATE TABLE transaction_articles
(
	id INT PRIMARY KEY AUTO_INCREMENT,
    id_article INT,
    type VARCHAR(50) NOT NULL,
    FOREIGN KEY (id_article) REFERENCES article(id_article)
);

CREATE TABLE handicap (
  id_handicap INT PRIMARY KEY AUTO_INCREMENT,
  id_user INT,
  hole_one INT,
  hole_two INT,
  hole_four INT,
  hole_five INT,
  hole_six INT,
  hole_seven INT,
  hole_eight INT,
  hole_nine INT,
  hole_ten INT,
  hole_eleven INT,
  hole_twelve INT,
  hole_thirteen INT,
  hole_fourteen INT,
  hole_fifteen INT,
  hole_sixteen INT,
  hole_seventeen INT,
  hole_eighteen INT,
  hole_nineteen INT,
  FOREIGN KEY (id_user) REFERENCES user(id_user)
);