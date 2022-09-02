Feature: ProductHandle


Scenario: ADD PRODUCT 
	Given The database is empty
	When I add product
	| Id | Name  | Value | ImagePath |
	| 1  | Test1 | 10.00 | FakePath1 |
	Then The result for add should be Success and the return should be 1	

	When I add product
	| Id | Name  | Value | ImagePath |
	| 2  | Test2 | 20.00 |			 |
	Then The result for add should be Error and the return should be Imagem é obrigatório
	
	When I add product
	| Id | Name | Value | ImagePath |       |
	| 3  |      | 30.00 | FakePath3 | Error |
	Then The result for add should be Error and the return should be Nome é obrigatório

	When I request for all products on the system
	Then The return for all products on the system should be
	| Id	 | Name				| Value			| ImagePath		|
	| 1		 | Test1			| 10.0			| FakePath1		|
	And The result for all products on the system should be Success 
	
Scenario: CHANGE PRODUCT 
	Given These products exists on the system for change		
	| Id	 | Name				| Value			| ImagePath		|
	| 1		 | Test1			| 10.00			| FakePath1		|
	| 2		 | Test2			| 20.00			| FakePath3		|
	When I change the product
	| Id | Name		| Value | ImagePath |
	| 2  | ALTERADO | 70.00 | FakePath1 |
	Then The result for change product should be Success

	When I change the product
	| Id | Name		| Value | ImagePath |
	| 5  | ALTERADO | 90.00 | FakePath1 |
	Then The result for change product should be Error
	
	When I request for all products on the system
	Then The return for all products on the system should be
	| Id | Name		| Value | ImagePath |
	| 1	 | Test1	| 10.0	| FakePath1	|
	| 2  | ALTERADO | 70.0  | FakePath1 |
	And The result for all products on the system should be Success 
	

	
Scenario: REMOVE PRODUCT 
	Given These products exists on the system for remove		
	| Id	 | Name		| Value			| ImagePath		|
	| 1		 | Test1	| 10.00			| FakePath1		|
	| 2		 | Test2	| 20.00			| FakePath3		|
	When I remove the product 2
	Then The result for remove product should be Success
	
	When I remove the product 3
	Then The result for remove product should be Error
	
	When I request for all products on the system
	Then The return for all products on the system should be
	| Id	 | Name		| Value			| ImagePath		|
	| 1		 | Test1	| 10.0			| FakePath1		|
	And The result for all products on the system should be Success
	