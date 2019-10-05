# avengers
Proyecto de desarrollo de una API web REST que consume la API de Marvel Comics
y expone las siguientes servicios:

1.	Los editores, escritores y coloristas que han estado involucrados en los cómics
	tanto de Iron Man como de Capitán América a través de sus respectivos endpoints:

http://localhost:44309/marvel/colaborators
http://localhost:44309/marvel/colaborators/ironman
http://localhost:44309/marvel/colaborators/capamerica

Un ejemplo de salida de este servicio es el siguiente:

{
	"last_sync": "Fecha de la última sincronización en dd/mm/yyyy hh:mm:ss",
	"editors": [
		"Wilson Moss",
		"Andy Smith",...
	],
	"writers": [
		"Ed Brubaker",
		"Ryan North",...
	],
	"colorists": [
		"Rico Renzi",
		"Frank D'ARMATA",...
	]
}
	

2.	Los heroes con los cuales Iron Man y Capitán América han interactuado en
	cada uno de los cómics a través de sus respectivos endpoints:

http://localhost:44309/marvel/characters
http://localhost:44309/marvel/characters/ironman
http://localhost:44309/marvel/characters/capamerica

Un ejemplo de salida de este servicio es el siguiente:

{
	"last_sync": "Fecha de la última sincronización en dd/mm/yyyy hh:mm:ss",
	"characters": [
		{
			"character": "Squirrel Girl",
			"comics": [
				"The Unbeatable Squirrel Girl (2015) #38",
				"The Unbeatable Squirrel Girl (2015) #39"
			]
		},
		{
			"character": "Jocasta",
			"comics": [
				"Tony Stark: Iron Man (2018) #2",
				"Tony Stark: Iron Man (2018) #3",...
			]
		},...
	]
}

