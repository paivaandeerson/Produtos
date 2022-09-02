Feature: Product

	
Scenario: RETRIEVE ALL PRODUCTS
	Given These products exists on the system for retrieve all		
		| Id	 | Name				| Value			| ImagePath		|
		| 1		 | Test1			| 10.00			| FakePath1		|
		| 2		 | Test2			| 20.00			| FakePath3		|

	When I request for all products
	Then The return for for all products should be
		| Id	 | Name				| Value			| ImagePath		|
		| 1		 | Test1			| 10.0			| FakePath1		|
		| 2		 | Test2			| 20.0			| FakePath3		|
	And The result for retrieve should be Success 

Scenario: RETRIEVE PRODUCT
	Given These products exists on the system for retrieve one		
		| Id	 | Name				| Value			| ImagePath		|
		| 1		 | Test1			| 10.00			| FakePath1		|
		| 2		 | Test2			| 20.00			| FakePath3		|

	When I request for Id 1	
	Then The return for request of product should be
		| Id	 | Name				| Value			| ImagePath		|
		| 1		 | Test1			| 10.0			| FakePath1		|
	And The result for retrieve should be Success 
