CIS 3342 Team Project – Core MVC Web Application & Web APIs
This project will give you experience utilizing all the techniques and concepts learned this semester. This project will be completed with teams of two that must be approved by me. 

ASP.NET Core MVC Requirement:
You must design and implement an ASP.NET Core MVC Web application. You will convert the application you built in Project 3 and improved upon for Project 4. This project will involve implementing Web APIs and an ASP.NET Core MVC that uses Razor Views instead of ASPX pages.


Login & Registration Requirements:
1.	Login
The opening screen allows an existing user to log-in to the Web application or create a new account registration.  Your application should allow for faster logins by storing and retrieving the user’s login information using a cookie. In the Login page, you will allow the user to store a cookie for faster logins and allow the user to delete an existing cookie.  The login process must also allow the user to retrieve a forgotten username and/or password.
Upon successful login or registration, proceed to the site user’s dashboard/homepage/landing page, which should include information, content, and tools related to the type of user account.  The application should also handle issues with the user bypassing your login and registration process. Add code to all the pages of your site to prevent someone from simply entering a URL for a page directly and bypass your login/registration. 
2.	Account Registration
The Registration process will construct a new user account. The user must supply a unique username, a password, email address, and other important contact information.  If your email address is the Login, then you require only the email address as the LoginID, but it must still be unique.  

Give the user the ability to save the LoginID to a cookie for faster logins. The registration process should require the user to answer security questions that will be used to retrieve their forgotten username and/or password. Your application should maintain at least 3 security questions and answers for each user and randomly choose one to verify the user before retrieving their lost login information. 

Implement a two-step verification where the application sends an email to the user’s email address on file along with some randomly generated verification code. The email should contain a link to either a Web API method or Razor View that will update their account to verified user. 

3.	Web Application Conversion 
Convert the ASP.NET Web Application built in Project 3 to an ASP.NET Core MVC Web Application. This will involve changing the application’s user interface from ASPX pages to Razor Views. You cannot use Razor Pages (alternative to Razor Views that wasn’t taught in class).

4.	Web API Requirements 
This project requires building at least 2 separate Web APIs that will be used by your Web application. You should build your Web API to perform a highly cohesive set of functions and not simply do everything from one Web API. 

1.	You must create and use an ASP.NET Core RESTful Web API in your project. The Web API must implement the HTTP Get, Post, Put, and Delete REST operations. 
2.	The Web APIs must make use of custom user-defined types and exchange data using JSON/XML. You cannot pass simple data types (string, int, double, etc…) to every Web API method. Your Web API must contain methods that accept custom user-defined types (class-based data type). You should build your Web API to be flexible and have several different versions of methods to allow developers to send Form Data and Complex objects (JSON/XML).
3.	The Web application (Razor Views) must contain server-side code to make Web requests to these Web APIs. This means your server-side code must be the consumer of the Web APIs and not rely on client-side code with AJAX to communicate with them. 

5.	New Third-Party Web API Requirement
This project requires that your Web application utilizes another Web API created by a third-party to demonstrate your knowledge of using other Web APIs that were not created by you. The use and implementation of a third-party Web API must be approved by me. 

6.	Navigation System
Provide a professional looking navigation system on each page to help the user navigate your site.

7.	Stored Procedures
Stored procedures should be used for all database operations. They should be used in all situations that require inputs to eliminate the SQL injection threat.

8.	Multiple Images 
The application must be able to handle multiple images for each dating profile. Implement the ability to allow the user to upload multiple images for their profile. This will also require updating the profile display to include a photo gallery. The photo gallery will only be displayed when the user is viewing the detailed view of a profile. The application shouldn’t display the photo gallery when viewing the summary profile data for search results, liked list, matches list, etc…

9.	Dynamic Data Displays
The Web application must make use of dynamic data displays in the Razor Views. This is one of the most important aspects of the Web application, so I expect a significant amount of thought and effort put into the design and display of the display of all content for the application. The most important content presentation includes the display of profiles, and photo gallery. The presentation of the content must be professional-looking and interesting. Simple displays that are difficult to use will not receive any credit. 

*** Important: every team is required to meet with me to discuss your application’s design and presentation at least 1 week before the project’s due date to get feedback on this aspect. We will review what you have done in design of the previous project and what you implemented in the current project. Based on my feedback from our team meeting, you will be required to apply the design and presentation feedback.

10.	Security
The Web application must use a secure method of storing sensitive data like passwords, credit card numbers, social security numbers, etc... The Web application must make use of encryption. You must use encryption in a different way than demonstrated in class examples.

11.	Serialization
The Web application must use serialization and deserialization for data persistence. The Web APIs use of serialization does not count toward this requirement since it is automatically done for you. You will need to find a creative way to use serialization to store and retrieve data for your Web application and it must be different than the class examples. 

12.	Learning Opportunity
This requirement involves learning about something new related to ASP.NET Core MVC and implementing it in your Web application. Each team member must do some research, find something new and useful, and implement it in your Web Application. For example, you can implement charts that are dynamically generated and displayed based on your database data. Another possible learning opportunity would be to build a SOAP ASMX Web Service and use it in your Web application. 
 
13.	CSS Styling
The Web application must use CSS to make your site look professional and attractive. The application must make use of images on all pages to make the site look attractive, interesting, and professional. You can write your own CSS code, or you may use BootStrap.

14.	Style Your CIS3342 Homepage
You need to update your homepage for this course. Make sure there are written descriptions of each project including a link to the published application. Finally, style this homepage to make it look professional and attractive.

15.	Contribute an Equal Amount 
You must split the work equally for each element of this project. I expect each member to contribute an equal amount of work to all elements of the project. For example, you should not divide the work in such a way that one person is responsible for the entire database and the other is responsible for the GUI design; each person should do half of the work on design and half of the work on the database. Therefore, each member should contribute half of the work towards the GUI, database, Web APIs, and all other required elements of this project.

16.	Overall Design 
Points will be awarded for various aspects of design which include, but not limited to:

1.	Most effective implementation of the step with clear instructions for the user.  For example, giving the user a drop-down box to select a state instead of entering the state’s name in a text box.

2.	Good navigation among pages.  Requiring the user to use the Browser Back and Forward buttons is extremely poor design.  On each page, the user should have every logical option of moving around the site.  

3.	Appropriate Client and Server-side validation, with precedence being given to server-side wherever possible. You must use server-side input validation and error-handling so that your applications do not crash for any reason. You aren’t required to write client-side JavaScript. 

4.	Good placement and sizing of labels and controls.  For example, you should not place a control with absolute positioning underneath a dynamically populated list (gridview, dropdownlist, etc.).  When the control expands, it may cover the control.  Another example is leaving extraneous controls on the screen.  This occurs when you do not make proper use of visibility.

5.	Clear instructional labels that help the user to complete a task and clear error messages.  

6.	Good presentation of the Web pages and their content regarding layout, alignments, and presentation. You may use BootStrap, CSS, or similar tools to help you build the design of your site.

7.	You must use component-based software design. This means creating code that is reusable by writing as much of the code as possible in classes and functions of classes instead of in the GUI or controller classes used to handle the GUI.
Due:
See the assignment posting under the Assignments section in Canvas.


Submission:
You need to publish your Web application project to the cis-iis2 Web server folder TermProject, upload the zip file containing the solution with all your code to Canvas, provide a URL to your Web application’s start page, and provide a written submission comment with the necessary information to locate and grade the requirements (see below).  
Additional Submission Requirements:
You must provide a written submission comment in Canvas must indicate how you implemented each of the above requirements and provide the URL of the page(s) that contain the requirement. If you do not provide this information in the submission comment before the deadline with all the necessary information to locate these hard-to-find requirements, you will not receive credit for the requirement, no exceptions!!!

You must populate the database with at least 30 dating profiles that contain data for all profile elements (likes, dislikes, goals, etc…).

You need to zip the root folder for your solution into a single zip file and submit the assignment in Canvas. To submit the assignment, you need to click the Assignment’s Title “Term Project” to view the submission form and upload the file. 
Make sure you properly submit your assignment and that it works. Programs that don’t run or don’t contain all the necessary files will not be graded nor will they receive credit, no exceptions.
