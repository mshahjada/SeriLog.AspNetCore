# App.SerilLog


Log event:

Verbose - tracing information and debugging minutiae; generally only switched on in unusual situations

Debug - internal control flow and diagnostic state dumps to facilitate pinpointing of recognised problems

Information - events of interest or that have relevance to outside observers; the default enabled minimum logging level

Warning - indicators of possible issues or service/functionality degradation

Error - indicating a failure within the application or connected system

Fatal - critical errors causing complete failure of the application


N.B: you can override default provided log event when a http request is made.




#Unusual terms that seri log uses to refer .NET objects map to internal representation

Stringification: ToSting()

Destructuring: Taking Complex .NET object and Convert into a structure which can be represented as JSON/XML

Scalars: .NET Types




#Showing a list in log

Log.Information("Data: {$ListItem}", new { Items: YourCollection })
