Preflight Checklist
===
* Ignore packages folder
* Ignore *.ncrunchsolution
* Setup Demo Branch
* Configure Build Server to monitor Demo Branch
* Clear NuGet package cache
* Warm up Visual Studio

Resources
===
* LLewellyn's Video: [Using ApprovalTests in .Net 20 Asp.Mvc Views](http://www.youtube.com/watch?v=SttlPzwJw3U)
* Chris's Video: [Approval Tests ILoader Example](http://www.youtube.com/watch?v=5gIeJ6z82Pk)

Introduction
===
* Unit testing MVC Views not easy at first.
* ApprovalTests make it possible.
* It's easy to repeat once you know how.
* Plan
	* File-> New Project
	* Setup Basic Tests
	* Setup Data-Dependent Tests
	* Make sure everything works in:
		* Visual Studio
		* NCrunch
		* CruiseControl.NET
	* Make a few mistakes along the way, resolve them.
* [GitHub Repository](https://github.com/jamesrcounts/aspConf-demo)

Project Setup
===

MVC3 Intranet Site
---
### Works fine with Internet Site
### Any ViewEngine
### Create MSTest project
#### ApprovalTests works with many popular frameworks
### Enable package restore

Choose Web Server
---

### ApprovalTests can approve any accessible web page.
### Choose a server to make your views accessible to ApprovalTests
* Cassini (easiest to get started)
* IIS
* IIS Express
* CassiniDev
* Testing Configuration Must Allow Anonymous Requests

Run the Default Tests
---

### Intranet template includes two Controller tests
* These pass in Visual Studio
* Configure and Run NCrunch, They Pass
* Commit, Push and Force Build

### Closer look at controller tests
### MVC Makes controller and Model Tests Easy
### ViewResult != View

View Tests
---

### Users care about the view
### We write the view, we should test the view
* Protection from Regression
* Design with feedback
* Executable specification

### Why not?  Too hard?  No interaction?

###Testing Views With ApprovalTests
* `Install-Package ApprovalTests -projectname CarDealership.Tests`
* Create `Views` folder to hold tests
* Add new `Basic Unit Test` called `HomeViewsTest` (`CTRL-SHIFT-A`)
* Import namespaces
	* `using ApprovalTests.Asp;`
	* `using ApprovalTests.Asp.Mvc;`
	* `using ApprovalTests.Reporters;`
* Decorate class with `UseReporterAttibute`
	* `FileLauncherReporter` allows you to review the rendered result.
	* `TortiseDiffReporter` quickly pinpoints small differences in HTML
		* ApprovalTests supports many diff tools out of the box
		* Adding your own is easy.  
		* I've written two, `CodeCompareDiffReporter` and `VisualStudioReporter` (alpha)
	* ProTip: `UseReporterAttibute` can have `assembly:` scope.
* Create `TestMethod` for each `View` to test.
	* `TestIndexView`
	* `TestAboutView`

###ApprovalTest Process in English
* `// Make test target available on web server`
* `// Supply route to MVC Action`
* `// Test action result (RED)` 
* `// Review result`
* `// Approve Result (if appropriate)`
* `// Run Again (GREEN)`
* `// Refactor`

###ApprovalTest Process in Action
* `CTRL+F5` to start the site.
* `Func<ActionResult> actionDelegate = new HomeController().Index;`
* `MvcApprovals.VerifyMvcPage(actionDelegate);`
* Failure: `MvcPort` not set, go back to "supply the route" and set `PortFactory.MvcPort`
* Failure: `ApprovalMissingException`, expected for first run of any ApprovalTest. 
* Run in `VS` to "review result"
* Notice that the browser shows us a local file.
* `TortoiseMerge` shows us the `HTML`
* Assume this is what we want, and approve.
	* Golden side will now be used for all future comparisons.
* Run Again to see test pass in VS
* NCrunch runs after deleting some comments, passes
* Refactor, not necessary for demo
	* Failing ApprovalTest provides design feedback.
	* Passing ApprovalTest provides protection from regression.
	* Same as any other TDD.
* Commit/Push
	* Ignore .received.* files.
* Refactor before going to second test.
	* Delete Comments
	* Inline delegate.
	* Move `MvcPort` to base class: (`SHIFT-ALT-C`) `MvcViewTest`
	* `TestIndexView` is now one line of code.
* Write and test `TestAboutView`
* Commit and Push

###Tackling the Build Server
* Test works fine in localhost (Visual Studio/NCrunch)
* Fails on build server
* First precondition for MvcApproval is not met 
* Test target is not visible from the build sever
* Build server must be able to make target available when needed
* Open source Web Server CassiniDev allows self-hosting in test.

###Add CassiniDev support
* `Install-Package CassiniDev -ProjectName CarDealership.Tests`
* Update MvcViewTest base class.
* Import namespace `using CassiniDev;`
* Add `[TestClass]` attribute
* Inherit `CassiniDevServer`
* Add Empty `Setup` and `Teardown` methods.
* Decorate `Teardown` with `[TestCleanup]`
* Decorate 'Setup' with `[TestInitialize]`
* Implement `Teardown` *first* using this line: `this.StopServer()`
* Implement `Setup` with this line.
	* `this.StartServer(@"..\..\..\CarDealership", PortFactory.MvcPort, "/", "localhost");`
* Failure, NCrunch indicates MVCPort is in use. 
* Same result in Visual Studio
* CassiniDev needs different port than Cassini
* `MvcPort++` for reasons that will become apparent.
* ApprovalFailure--Port# is part of the HTML
* We should approve the new port so the test will pass on CC.NET
* Note that CSS is missing from rendered file
* Self-hosting is a good solution for the build server
* Self-hosting makes design feedback less useful
* Switch back and forth as needed by controlling the port/host startup.
* After approval, test passes in Visual Studio, fails in NCrunch
* NCrunch has 500 error, path to `CarDealership` is different.
* Test relies on bad assumption about the Build environment.
* NCrunch revals this assumption.
* Use `ApprovalUtilities` to dynamically locate the MvcApplication.
* `this.StartServer(MvcApplication.Directory, PortFactory.MvcPort, "/", "localhost");`
* Use Refactoring tool to create `Directory`
* `return PathUtilities.GetDirectoryForCaller();`
* `Install-Package ApprovalTests -ProjectName CarDealership`
* You need `ApprovalUtilities` but you also get `ApprovalTests`.
* You can delete `ApprovalTests` if it bothers you.
* Import namespace `ApprovalUtilities.Utilities`
* NCrunch notices the new implementation 
* All tests pass in NCrunch.
* Run in Visual Studio
* May see 2 failures due to port conflict. 
* Race condition NCrunch vs Visual Studio.
* Run again.
* Commit and Push and Force Build
* Failure.  Pages contain user name in HTML, which is not the same on the server.
	* Update Views to remove user name
	* Approve.

###Working with Data
* Tests work in three environments
* Our tests views are static
* Real views are dynamic
* Lets add some data
* Create two classes
	* `CarLot` - Repository
	* `Car` - Entity
* `CarLot` always throws an exception.
* Create an Action that displays a list of cars.
	* `// Get the data`
	* `// Show the data`
* To Get Data, we need a repository
	* Implement default constructor
	* Implement injectable constructor
* Implement and Build
* Add strongly typed list view.
* Test it
	* Request fails with 500 error
	* Switch to desktop Cassini 
	* Problem is unavailable repository.
* Can't mock it, server has its own copy of `HomeController`.
* We must create a test seam to bypass.
	* Cars action does two things
		* Get the data
		* Show the data
	* To test, we need to reimplement Get The Data so that it doesn't use the repository
	* Separate Get the Data from Show The Data by using Extract Method to create a new private method
	* `private ViewResult Cars(IEnumerable<Car> theData){return View(theData);}`
	* This is the part that doesn't change.
	* Before implementing the part that does change, take steps to isolate
		* Make HomeController partial
		* Create file HomeControllerTest
		* Add other half of the partial to the HomeControllerTest file.
	* Implement 	`TestCars`
		* `public ViewResult TestCars(){...}`
		* This method will new up test data
		* Pass it to private seam.
	* Update test to call TestCars
* Test fails now because MVC looks for a view called `TestCars`
	* Update seam to call `View` with explicit name.
	* `return View("Cars", theData");`
* Now we get feedback from ApprovalTests.  
	* We can see that removing the login name has screweed up CSS
	* We don't care about that right now.
	* Approve
* Retest passes.
* Cleanup Magic String
	* Call with Explicit ViewName introduces magic string
	* Magic strings resist refactoring
	* Remove with Approval Utilities
		* `using ApprovalUtilities.Asp.Mvc;`
		* `return View(theData).Explicit();`
	* Protect From Inlining
		* Turn off optimization
		* Or Add Attribute
			* `[MethodImpl(MethodImplOptions.NoInlining)]`
			* `CTRL-.` to add `using System.Runtime.CompilerServices;`
* Exclude Test Code from Release Deployments
	* `HomeControllerTest` line 1: `#if DEBUG`
	* `HomeControllerTest` line last++: `#endif`
	* Switch to Release mode.
	* Creates compilation error in Test project. 
	* Exclude `TestCars` as well.
* Finally Reconfigure For Automated Tests
	* Protection from Regression When Not Desigining
	* Will let the tests pass on the build server.
	* Uncomment `PortFactory.MvcPort++;`
	* Uncomment `this.StartServer(...);`
	* Re-run tests.
	* Approve failing tests.
	* Watch out for race condition.
	* ReRun NCrunch
	* Commit and Push