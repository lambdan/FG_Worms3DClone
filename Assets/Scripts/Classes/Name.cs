using System.Collections.Generic;
using UnityEngine;

public static class Name
{
    public static string GetRandomName()
    {
        return _names[Random.Range(0, _names.Count)];
    }

    private static List<string> _names = new List<string>() // Names from https://randomwordgenerator.com/name.php
    {
        "Abram",
        "Ada",
        "Adam",
        "Alejandra",
        "Alen",
        "Alex",
        "Alexander",
        "Alfred",
        "Ali",
        "Allyson",
        "Amanda",
        "Anton",
        "Antony",
        "Arnold",
        "Arnulfo",
        "Arron",
        "Aubrey",
        "Aurelia",
        "Aurelio",
        "Bennett",
        "Bill",
        "Bob",
        "Bobbie",
        "Bonnie",
        "Brooks",
        "Bryon",
        "Carlton",
        "Carol",
        "Caroline",
        "Carroll",
        "Cecilia",
        "Charmaine",
        "Christian",
        "Christy",
        "Clarice",
        "Clayton",
        "Connie",
        "Cordell",
        "Cornelius",
        "Cruz",
        "Dan",
        "Dana",
        "Darnell",
        "Daron",
        "Daryl",
        "David",
        "Dawn",
        "Deanne",
        "Dee",
        "Delores",
        "Deloris",
        "Demetrius",
        "Derick",
        "Dewayne",
        "Dusty",
        "Dwight",
        "Edward",
        "Efren",
        "Elbert",
        "Elisabeth",
        "Elma",
        "Emile",
        "Emilio",
        "Emory",
        "Erich",
        "Erin",
        "Ezequiel",
        "Fay",
        "Felton",
        "Fermin",
        "Flora",
        "Frances",
        "Francine",
        "Francisco",
        "Frankie",
        "Franklyn",
        "Frederick",
        "Gaston",
        "Geneva",
        "Georgia",
        "Gilbert",
        "Gladys",
        "Graciela",
        "Guillermo",
        "Hai",
        "Hattie",
        "Helene",
        "Huey",
        "Hugh",
        "Hung",
        "Imelda",
        "Ingrid",
        "Ivy",
        "Jack",
        "Jackson",
        "Jacob",
        "Jaime",
        "Jane",
        "Janna",
        "Jasper",
        "Jeff",
        "Jeffery",
        "Jens",
        "Jeramy",
        "Jeromy",
        "Jerrod",
        "Joaquin",
        "Jonas",
        "Jonathan",
        "Josefina",
        "Julius",
        "Junior",
        "Karen",
        "Karin",
        "Kathrine",
        "Kathryn",
        "Kelly",
        "Keri",
        "Kimberly",
        "Kitty",
        "Kristin",
        "Lacy",
        "Lanny",
        "Lauren",
        "Laverne",
        "Lelia",
        "Leonard",
        "Letha",
        "Letitia",
        "Liam",
        "Lilly",
        "Linda",
        "Lindsay",
        "Lonnie",
        "Louis",
        "Louisa",
        "Luann",
        "Luigi",
        "Man",
        "Marc",
        "Marco",
        "Marcelino",
        "Margery",
        "Mario",
        "Marjorie",
        "Marla",
        "Marta",
        "Matilda",
        "Matthew",
        "Mauro",
        "Max",
        "Mildred",
        "Miles",
        "Millard",
        "Mollie",
        "Monty",
        "Nadine",
        "Nicolas",
        "Noel",
        "Ollie",
        "Paige",
        "Parker",
        "Patsy",
        "Priscilla",
        "Queen",
        "Randall",
        "Randi",
        "Rebecca",
        "Regina",
        "Renaldo",
        "Rickey",
        "Rogelio",
        "Roland",
        "Romeo",
        "Roxanne",
        "Ruby",
        "Ryan",
        "Sal",
        "Salvatore",
        "Sanford",
        "Saul",
        "Sergei",
        "Seymour",
        "Shane",
        "Sharron",
        "Sidney",
        "Sofia",
        "Sonia",
        "Stan",
        "Stanley",
        "Steve",
        "Susana",
        "Tabatha",
        "Tanya",
        "Tara",
        "Theresa",
        "Theron",
        "Tina",
        "Tisha",
        "Tobias",
        "Tommy",
        "Tonya",
        "Trevor",
        "Von",
        "Waldo",
        "Walter",
        "Whitney",
        "Yolanda"
    };
}