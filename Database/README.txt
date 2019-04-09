Hvordan man kører projektet:

1. Åbn projektet. 
2. Åbn BlackBoardContext.cs
3. Udkommenter linje 31.
4. brug egen connectionstring i stedet. Eksempel:
	var connectionString = "Data Source=<Indsæt Servernavn> ;Initial Catalog=<Indsæt Databasenavn>;Integrated Security=True";
	Hvis databasen ikke findes, vil den blive oprettet. 
5. Update-Database 
6. Ctrl+F5

Ovenstående vil lade en se de views der var krav til.
For at tilføje de ting der kræves, til databasen henvises til Progra.cs linje 17- 59.
Her vises, hvordan repositories'nes .Add() funktioner kan bruges. 


		uow.StudentRepository.Add(new Student()
            	{
            	    AU_ID = "au589973",
            	    Birthday = DateTime.Parse("07/02/1995"),
            	    Name = "Tobias Lund",
            	    EnrollmentDate = DateTime.Parse("01/01/2017"),
            	    GraduationDate = DateTime.Parse("01/01/2021"),
            	});
            	uow.Complete();