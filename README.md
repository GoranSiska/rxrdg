# What are Regular Expressions? (srsly?)
A language for creating concise descriptions of search patterns.

# What is rxrdg?
A lightweight C# utility for creating random data based on Regular Expressions like this:

	var param = @"(\([0]\d{2}\))(\d{6,7})";
	var rxrdg = new RegExpDataGenerator(param);

	for(var i=0; i<10; i++)
	{
   		Console.WriteLine(rxrdg.Next());
	}

# Why is this useful?
Imagine building an entry form. It's not unusual to have regexp validating input fields before they are submitted to API.

Using rxrdg in the same scenario will help you leverage existing validation rules to create test data.

It should be fairly simple to use rxrdg to create test data or even test objects even when no validation regexes exist.
