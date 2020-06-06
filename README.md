![Logo](https://user-images.githubusercontent.com/18705108/83945992-caefe180-a82b-11ea-9bed-0e49fa56a66f.png)

# iMatter Platform - Connecting and Empowering Lives

 

<h2>Team & Authors:</h2>

 

1.Rashmi Dastidar

2.Nirmala Devidasan

3.Tanya Maheshwari

4.Keerthi Gubba

5.Veena Chandrika

 

<h1>Overview</h1>

 

<h2>Problem Statement</h2>

 

COVID-19, apart from being a pandemic health crisis is a socio economic crisis which has affected people from all sectors of the society.The challenges faced are multifold. iMatter Platform is a humble attempt from us the, Volvo_CO[vi]DE WARRIOR, to battle against an invisible enemy that has disrupted our lives and challenged the normal.

 

The working mothers of young children, even those with flexible jobs,are straining under the pressure of round-the-clock “pandemic parenting” and eldercare.And while this situation poses challenges for men, too, it hits women harder.After all, women still undertake the disproportionate share of caregiving responsibilities, and the very infrastructure that makes full-time work possible for many mothers—assistance from relatives, daycare, school—has, for the first time in at least a century, fully shut down in many countries around the world.They are stressed right now about pretty much everything, including how extended school closures will affect their kids’ learning, ensure that the health concerns of the family and self are addressed, without a risk of exposure to the novel corona virus and also continue to support the financial needs of the family while giving back to the society as well.

 

Teachers also need socio-emotional support to face the pressure being put to deliver learning in a time of crisis as well as support their students’ emotional needs.Online teaching has turned up as a massive shock for both teacher’s productivity and also student’s social life and learning.For the differently abled and for the underprivileged, the impact is even more and every citizen has a moral and social responsibility to ensure that food,education and health remains accessible.Parents, teachers and students also should stay mentally and physically active and healthy.

 

The world cannot forever, remain under lockdown and we will one day have to open up so that our economy does not collapse. While doing so, we have to ensure that we continue to maintain the social distancing and other best practices we follow today so as to ensure that the society remains unaffected.

<h2>Idea and Solution</h2>

 

It is a RemoteEverything world that we are in today. The iMatter platform digitalizes the health and education activities and also ensures that it remains accessible even to the differently abled and the underpriveleged . Teachers can take online classes, conduct quiz, upload notes, videos , monitor students performance and remotely connect with the parents and students there by ensuring that they stay focused.

 

The Watson assistant can help the users stay informed and connected 24 X 7. Students can make use of the Watson assistant to revise the portions and for self assessment. Watson can also address queries related to health and nutrition.The video transcribe service used helps students to download lecture notes even if they have missed the classes. The multilingual feature which we have added helps students across the nation to make use of the online lectures without any language barriers.It also ensures that the differently abled children’s educational needs are addressed. The quiz app will help the teacher to assess the students regularly and parents can monitor their wards progress. A solution which will help maintain social distancing and usage of face masks is also provided which is integrated to a calling functionality so that in case of a violation, an alarm is raised and necessary actions can be taken. To ensure that the society stays healthy, physically and mentally, we have linked our application to online health services. Useful links to create AR VR virtual labs for students are provided so as to aid the teacher to deliver lectures

 

We as citizens, have a social responsibility too and our platform ensures that education remains accessible to all, irrespective of their financial background and their location .Our platform makes use of a service which will help collect spare smart devices which can be used by the needy . There is a provision to contribute towards ensuring food for all. Inorder to ensure that women stay empowered our platform provides startup information for the women entrepreneurs in making.

 

<h2>Technology</h2>

 

We used mobile,web,cloud and server side technologies for the rapid development and deployment of applications.To build, train, and deploy conversational interactions we used Watson Assistant,Discovery and other cognitive services offered by IBM.IBM Digital App Builder was used for Hybrid Mobile Application Development. The backend data sources were IBM Cloud Storage , CLoudant DB and we used the IPaaS platform from IBM, App Connect to frontend the data sources thereby, making the integration seamless.

 

<h2>Architecture Diagram </h2>

 

<h2>Web:</h2>

 

![Web Application Architecture](https://user-images.githubusercontent.com/18705108/83942863-1b0f7980-a815-11ea-9109-544ecb143b43.JPG)

 

<h2>Web Application Workflow</h2>

 

1.            Our solution is accessible @ http://imatter.azurewebsites.net/.<br/>

   We have provided a role based access control for the parent, student,teacher and the admin.The below credentials can be used to try    out our solution<br/>

Password to be used across all roles-  (password - Password@123). <br/>

The corresponding user ids are as below<br/>

  i.           Teacher - ID-(physics.teacher1@gmail.com) <br/>

  ii. Student- ID-(student.one@gmail.com)<br/>

  iii.Parent-ID -(parent.one@gmail.com)<br/>

  iv.         Admin-ID- (admin.one@gmail.com)<br/>

 

Basic windows authentication with individual user authentication is used. <br/>

2.            We have provided the following features for the teacher role- <br/>

     a.       Create tutorial, upload video content, PDFs<br/>

     b.      Create quiz and upload questions. <br/>

     c.       See the report of each student <br/>

     d.      Talk to watson chatbot for quick help regarding heath and education<br/>

     e.      Access useful links, give back to society via the social responsibility links provided, Create AR/VR Content  for virtual labs   etc. <br/>

Technolgy and Services Used are IBM Watson Assistant ,Discovery, NLU, Translation Services from IBM etc<br/>

 

3.            For the Student, we have made provisions for-<br/>

     a.       Accessing tutorials and study materials uploaded by teachers.<br/>

     b.      Get the video uploaded by the faculty with multilingual transcription<br/>

     c.       Take assessment  and get the assessment reports on the spot<br/>

     d.      Quick help from Watson Chatbot which will serve as a knowledge and learning assistant<br/>

4.            For the Parent, the features provided are <br/>

     a.       Monitor their ward’s performance<br/>

     b.      Access useful links, and contribute towards ensuring education and meals to the needy.<br/>

     c.       Women Entrepreneurship information for empowering women by providing opportunities for enhancing skill and procuring monetary aids.<br/>

     d.      Accessing health care services from the comfort of their home.<br/>

     e.      Quick help from Chatbot regarding queries on staying fit physically and mentally, admission process etc.<br/>

5.            For the Admin, we have provided some unique features to enable the society to come out of the lockdown while ensuring protection from infection. <br/>

     We have used computer vision and opensource machine learning libraries, Python libraries and opensource IDEs for coming up with the solution.<br/>

This module enables the admin to make use of the live streaming survelliance which automatically detects if face masks and social distancing is maintained and in case of violation the admin will automatically get a call and trigger the alarm in the premises<br/>

 

<h2>Mobile:</h2>

 

![Mobile Application Architecture](https://user-images.githubusercontent.com/18705108/83942892-5611ad00-a815-11ea-8f28-3b87ef776680.JPG )

 

<h2>Mobile Application Workflow</h2>

 

•             User launches the mobile app, enters his/her credentials on the login screen and clicks Login.<br/>

•             Mobile app sends the user credentials to MFP server for validation.<br/>

•             MFP server invokes the security adapter logic to validate user credentials and returns an appropriate response to the mobile app. For the sake of this demo, we will use a simple security adapter that returns success when password equals username.<br/>

•             If user authentication succeeds, mobile app proceeds to show the home page where the tiles for the different roles such as Teacher, Student and Parent are displayed. <br/>

•             For pages like Physician Details, Social Responsibility etc we are using IBM storage and API services. MFP adapter fetches the data from Cloudant and returns it to the mobile app. The data fetched from Cloudant will have references to the images stored in Cloud Object Storage.<br/>

•             Backend integration APIs are developed using Citizen Integration capabilities that IBM AppConnect provides to integrate with Google Spreadsheet. <br/>

•             MFP API adapter is used to connect to the public API exposed from IBM AppConnect to retrieve the details.<br/>

 

<h2>IBM Services Used </h2>

 

IBM Cloud Foundry<br/>

IBM Watson Assistant<br/>

IBM Watson Discovery<br/>

IBM App Connect<br/>

IBM Natural Language Understanding (NLU)<br/>

IBM Cloud Object Storage<br/>

IBM Cloudant<br/>

IBM Digital App Builder (DAB)<br/>

Mobile Foundation Server<br/>

IBM Watson Studio<br/>

IBM Language Translation<br/>

 

<h2>SourceCode</h2>

 

Web Application -Refer -<EduCarePortal><br/>

 

Mobile Application -Refer -<Mobile App.zip><br/>

Limiting Crowd Solution -Refer -<No_Of_People_Counting.zip><br/>

 

Face Mask Detection -Refer -<FaceMaskDetection.zip><br/>

 

Social Distancing Solution -Refer-<SocialDistancing.zip><br/>

Overall Solution Video -Refer-<><br/>

 

Additional Videos Showcasing Application Demo -Refer-<><br/>

 

<h2>Challenges Faced during Mobile Application Development:</h2>

 

We have tried to use the IBM Cloud Platform and associated services in our solution to maximum.

The documentation and the enablement sessions provided helped us to shorten the learning curve.

Though the team had very less expertise in mobile application development, we were able to manage to come up with a mobile in this short span of time as we used The IBM Digital App Builder. However there were lot of limitations in the tool due to which we had to restrict the scope. We created the ionic app using the IBM Digital App Builder(DAB) and since some features in our usecase were not achievable with DAB, we built an app using Android studio and react native, which incorporated web views, video embedding and so on. We faced some challenges initially in incorporating the Security, REST API & Storage adapters but we were able to overcome the same eventually.

We followed the same approach in the Web Application development also & have tried to incorporate maximum IBM provided cloud services.

 

<h2>The Solution Road Map</h2>

 

For the web application we would like to bring in additional features like live streaming with thermal camera for temperature detection.We would like to host the computer vision module and other application modules in the IBM cloud. Our objective is to build a complete platform which will fulfull our vision to ensure education,health,food and women empowerment.

 

In the mobile application we are using a basic security adapter today, but we would like to enhance it for OAuth/LDAP based authentication.We would like to work on incorporating features which would enable collaboration. We would also like to work on improvising the screens and optimizing the code, performance and other NFRs.We would also like to enhance the application by including some of the features we have in our web application today.

 

<h2>References</h2>

 

IBM Watson Assistant <br/>

How-to guides for chatbots <br/>

Learning path: Getting started with Watson Assistant <br/>

Build a Chatbot for Your Mobile App <br/>

Building successful mobile apps article series<br/>