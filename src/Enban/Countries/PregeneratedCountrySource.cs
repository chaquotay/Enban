using System.Collections.Generic;

namespace Enban.Countries {

	public class PregeneratedCountrySource : ICountrySource {
	
		private static readonly Dictionary<string, Country> CountriesByCode = new Dictionary<string, Country>();

		static PregeneratedCountrySource() {
				CountriesByCode["AD"] =  new Country(
					"AD",
					"Andorra",
					// 4!n4!n12!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 12, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["AE"] =  new Country(
					"AE",
					"The United Arab Emirates",
					// 3!n16!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 16, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["AL"] =  new Country(
					"AL",
					"Albania",
					// 8!n16!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 8, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 16, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["AT"] =  new Country(
					"AT",
					"Austria",
					// 5!n11!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 11, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["AZ"] =  new Country(
					"AZ",
					"Azerbaijan",
					// 4!a20!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 20, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["BA"] =  new Country(
					"BA",
					"Bosnia and Herzegovina",
					// 3!n3!n8!n2!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 8, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["BE"] =  new Country(
					"BE",
					"Belgium",
					// 3!n7!n2!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 7, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["BG"] =  new Country(
					"BG",
					"Bulgaria",
					// 4!a4!n2!n8!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 8, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["BH"] =  new Country(
					"BH",
					"Bahrain",
					// 4!a14!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 14, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["BR"] =  new Country(
					"BR",
					"Brazil",
					// 8!n5!n10!n1!a1!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 8, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 10, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 1, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 1, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["BY"] =  new Country(
					"BY",
					"Republic of Belarus",
					// 4!c4!n16!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 16, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["CH"] =  new Country(
					"CH",
					"Switzerland",
					// 5!n12!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 12, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["CR"] =  new Country(
					"CR",
					"Costa Rica",
					// 4!n14!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 14, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["CY"] =  new Country(
					"CY",
					"Cyprus",
					// 3!n5!n16!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 16, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["CZ"] =  new Country(
					"CZ",
					"Czech Republic",
					// 4!n6!n10!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 6, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 10, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["DE"] =  new Country(
					"DE",
					"Germany",
					// 8!n10!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 8, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 10, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["DK"] =  new Country(
					"DK",
					"Denmark",
					// 4!n9!n1!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 9, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 1, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["DO"] =  new Country(
					"DO",
					"Dominican Republic",
					// 4!c20!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 20, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["EE"] =  new Country(
					"EE",
					"Estonia",
					// 2!n2!n11!n1!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 11, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 1, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["ES"] =  new Country(
					"ES",
					"Spain",
					// 4!n4!n1!n1!n10!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 1, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 1, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 10, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["FI"] =  new Country(
					"FI",
					"Finland",
					// 3!n11!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 11, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["FO"] =  new Country(
					"FO",
					"Faroe Islands",
					// 4!n9!n1!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 9, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 1, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["FR"] =  new Country(
					"FR",
					"France",
					// 5!n5!n11!c2!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 11, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["GB"] =  new Country(
					"GB",
					"United Kingdom",
					// 4!a6!n8!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 6, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 8, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["GE"] =  new Country(
					"GE",
					"Georgia",
					// 2!a16!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 2, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 16, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["GI"] =  new Country(
					"GI",
					"Gibraltar",
					// 4!a15!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 15, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["GL"] =  new Country(
					"GL",
					"Greenland",
					// 4!n9!n1!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 9, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 1, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["GR"] =  new Country(
					"GR",
					"Greece",
					// 3!n4!n16!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 16, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["GT"] =  new Country(
					"GT",
					"Guatemala",
					// 4!c20!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 20, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["HR"] =  new Country(
					"HR",
					"Croatia",
					// 7!n10!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 7, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 10, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["HU"] =  new Country(
					"HU",
					"Hungary",
					// 3!n4!n1!n15!n1!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 1, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 15, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 1, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["IE"] =  new Country(
					"IE",
					"Ireland",
					// 4!a6!n8!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 6, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 8, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["IL"] =  new Country(
					"IL",
					"Israel",
					// 3!n3!n13!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 13, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["IQ"] =  new Country(
					"IQ",
					"Iraq",
					// 4!a3!n12!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 12, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["IS"] =  new Country(
					"IS",
					"Iceland",
					// 4!n2!n6!n10!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 6, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 10, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["IT"] =  new Country(
					"IT",
					"Italy",
					// 1!a5!n5!n12!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 1, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 12, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["JO"] =  new Country(
					"JO",
					"Jordan",
					// 4!a4!n18!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 18, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["KW"] =  new Country(
					"KW",
					"Kuwait",
					// 4!a22!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 22, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["KZ"] =  new Country(
					"KZ",
					"Kazakhstan",
					// 3!n13!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 13, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["LB"] =  new Country(
					"LB",
					"Lebanon",
					// 4!n20!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 20, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["LC"] =  new Country(
					"LC",
					"Saint Lucia",
					// 4!a24!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 24, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["LI"] =  new Country(
					"LI",
					"Liechtenstein",
					// 5!n12!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 12, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["LT"] =  new Country(
					"LT",
					"Lithuania",
					// 5!n11!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 11, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["LU"] =  new Country(
					"LU",
					"Luxembourg",
					// 3!n13!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 13, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["LV"] =  new Country(
					"LV",
					"Latvia",
					// 4!a13!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 13, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["MC"] =  new Country(
					"MC",
					"Monaco",
					// 5!n5!n11!c2!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 11, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["MD"] =  new Country(
					"MD",
					"Moldova",
					// 2!c18!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 2, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 18, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["ME"] =  new Country(
					"ME",
					"Montenegro",
					// 3!n13!n2!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 13, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["MK"] =  new Country(
					"MK",
					"Macedonia",
					// 3!n10!c2!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 10, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["MR"] =  new Country(
					"MR",
					"Mauritania",
					// 5!n5!n11!n2!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 11, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["MT"] =  new Country(
					"MT",
					"Malta",
					// 4!a5!n18!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 18, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["MU"] =  new Country(
					"MU",
					"Mauritius",
					// 4!a2!n2!n12!n3!n3!a
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 12, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 3, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["NL"] =  new Country(
					"NL",
					"The Netherlands",
					// 4!a10!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 10, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["NO"] =  new Country(
					"NO",
					"Norway",
					// 4!n6!n1!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 6, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 1, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["PK"] =  new Country(
					"PK",
					"Pakistan",
					// 4!a16!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 16, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["PL"] =  new Country(
					"PL",
					"Poland",
					// 8!n16!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 8, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 16, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["PS"] =  new Country(
					"PS",
					"State of Palestine",
					// 4!a21!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 21, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["PT"] =  new Country(
					"PT",
					"Portugal",
					// 4!n4!n11!n2!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 11, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["QA"] =  new Country(
					"QA",
					"Qatar",
					// 4!a21!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 21, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["RO"] =  new Country(
					"RO",
					"Romania",
					// 4!a16!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 16, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["RS"] =  new Country(
					"RS",
					"Serbia",
					// 3!n13!n2!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 13, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["SA"] =  new Country(
					"SA",
					"Saudi Arabia",
					// 2!n18!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 18, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["SC"] =  new Country(
					"SC",
					"Seychelles",
					// 4!a2!n2!n16!n3!a
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 16, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 3, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["SE"] =  new Country(
					"SE",
					"Sweden",
					// 3!n16!n1!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 16, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 1, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["SI"] =  new Country(
					"SI",
					"Slovenia",
					// 5!n8!n2!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 8, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["SK"] =  new Country(
					"SK",
					"Slovakia",
					// 4!n6!n10!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 6, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 10, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["SM"] =  new Country(
					"SM",
					"San Marino",
					// 1!a5!n5!n12!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 1, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 12, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["ST"] =  new Country(
					"ST",
					"Sao Tome and Principe",
					// 4!n4!n11!n2!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 11, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["SV"] =  new Country(
					"SV",
					"El Salvador",
					// 4!a20!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 20, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["TL"] =  new Country(
					"TL",
					"Timor-Leste",
					// 3!n14!n2!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 14, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["TN"] =  new Country(
					"TN",
					"Tunisia",
					// 2!n3!n13!n2!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 3, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 13, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["TR"] =  new Country(
					"TR",
					"Turkey",
					// 5!n1!n16!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 5, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 1, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 16, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["UA"] =  new Country(
					"UA",
					"Ukraine",
					// 6!n19!c
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 6, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.AlphanumericCharacters, 19, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["VG"] =  new Country(
					"VG",
					"Virgin Islands",
					// 4!a16!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.UpperCaseLetters, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 16, Text.LengthIndication.Fixed),
												}
					)}
				);
				CountriesByCode["XK"] =  new Country(
					"XK",
					"Kosovo",
					// 4!n10!n2!n
					new Text.AccountNumberFormatInfo{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{
													new Text.Segment(Text.CharacterClass.Digits, 4, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 10, Text.LengthIndication.Fixed),
													new Text.Segment(Text.CharacterClass.Digits, 2, Text.LengthIndication.Fixed),
												}
					)}
				);
		}

		public IEnumerable<string> GetCodes() => CountriesByCode.Keys;

		public Country ForCode(string code) {
			Country c = null;
			if(!string.IsNullOrWhiteSpace(code)) {
				CountriesByCode.TryGetValue(code, out c);
			}
			return c;
		}
		
	}
}

