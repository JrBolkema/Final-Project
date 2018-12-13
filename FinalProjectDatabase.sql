CREATE TABLE Users (
	UserID int PRIMARY KEY IDENTITY,
	Username varchar(25),
	Password varchar(25)
	);

CREATE TABLE Workouts(
	UserID int FOREIGN KEY REFERENCES Users(UserID),
	Date date DEFAULT(CONVERT(date,CURRENT_TIMESTAMP)),
	PRIMARY KEY(UserID,Date),
	WorkoutName varchar(25),
	Sets int,
	Reps int,
	Weight int
);