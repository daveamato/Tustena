# TUSTENA
## OPEN SOURCE INSTALLATIONS STEPS
### PREREQUISITES: Microsoft SQL SERVER o MSDE or SQLEXPRESS2005

1.	Create a folder “tustenaOS” in the “wwwroot” folder (if it exists)
2.	Extract all the files from the “Tustena_CRM_XXXX.zip” into the “tustenaOS” folder (the one you just created).
3.	Configure the Internet Information Server to point at “wwwroot/TustenaOS” by following this steps:
a.	Open the IIS (“control Panel” -> “Administration tools” -> “Internet Information Services”)
b.	Open the “Predefined Web-Site” branch from the tree menu on the left.
c.	Open the “Predefined Web-Site” properties window by Right-Clicking in it and selecting “Properties”.
d.	On the properties windows select the “Home Directory” Tab,  write “C:\Inetpub\wwwroot\TustenaOS” in the “Local path” textbox and press “Ok”.

4.	Open Microsoft SQL Server and in the “Microsoft Query Analyzer” open "tustenaos_final.sql" (located in the tustenaos folder). 
5.	Execute the query (Press F5) to create the database structure and fill-in the required data. 
6.	Open the “web.config” with any text editor and complete the connection string by entering your SQL Server name, user id and password (note: the connection string is the one under “Your string connection” line)
7.	In the same file fill-in all the required AppSettings like email addresses, SMTP server and DNS.
8.	Check if the “State Server” is running, otherwise execute it. 
 
First time login username and password are: “admin@admin.com”/ "admin”. 
You MUST change them to prevent intrusions.

In case you may encounter any difficulties installing TUSTENA CRM Open source, a support package is available at www.tustena.com.

More information are available on http://www.tustena.com/wiki