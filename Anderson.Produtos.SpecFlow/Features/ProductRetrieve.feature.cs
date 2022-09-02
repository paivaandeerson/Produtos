﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Anderson.Produtos.SpecFlow.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class ProductFeature : object, Xunit.IClassFixture<ProductFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "ProductRetrieve.feature"
#line hidden
        
        public ProductFeature(ProductFeature.FixtureData fixtureData, Anderson_Produtos_SpecFlow_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "Product", null, ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="RETRIEVE ALL PRODUCTS")]
        [Xunit.TraitAttribute("FeatureTitle", "Product")]
        [Xunit.TraitAttribute("Description", "RETRIEVE ALL PRODUCTS")]
        public void RETRIEVEALLPRODUCTS()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("RETRIEVE ALL PRODUCTS", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 4
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Name",
                            "Value",
                            "ImagePath"});
                table11.AddRow(new string[] {
                            "1",
                            "Test1",
                            "10.00",
                            "FakePath1"});
                table11.AddRow(new string[] {
                            "2",
                            "Test2",
                            "20.00",
                            "FakePath3"});
#line 5
 testRunner.Given("These products exists on the system for retrieve all", ((string)(null)), table11, "Given ");
#line hidden
#line 10
 testRunner.When("I request for all products", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Name",
                            "Value",
                            "ImagePath"});
                table12.AddRow(new string[] {
                            "1",
                            "Test1",
                            "10.0",
                            "FakePath1"});
                table12.AddRow(new string[] {
                            "2",
                            "Test2",
                            "20.0",
                            "FakePath3"});
#line 11
 testRunner.Then("The return for for all products should be", ((string)(null)), table12, "Then ");
#line hidden
#line 15
 testRunner.And("The result for retrieve should be Success", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="RETRIEVE PRODUCT")]
        [Xunit.TraitAttribute("FeatureTitle", "Product")]
        [Xunit.TraitAttribute("Description", "RETRIEVE PRODUCT")]
        public void RETRIEVEPRODUCT()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("RETRIEVE PRODUCT", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 17
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Name",
                            "Value",
                            "ImagePath"});
                table13.AddRow(new string[] {
                            "1",
                            "Test1",
                            "10.00",
                            "FakePath1"});
                table13.AddRow(new string[] {
                            "2",
                            "Test2",
                            "20.00",
                            "FakePath3"});
#line 18
 testRunner.Given("These products exists on the system for retrieve one", ((string)(null)), table13, "Given ");
#line hidden
#line 23
 testRunner.When("I request for Id 1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Name",
                            "Value",
                            "ImagePath"});
                table14.AddRow(new string[] {
                            "1",
                            "Test1",
                            "10.0",
                            "FakePath1"});
#line 24
 testRunner.Then("The return for request of product should be", ((string)(null)), table14, "Then ");
#line hidden
#line 27
 testRunner.And("The result for retrieve should be Success", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                ProductFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                ProductFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion