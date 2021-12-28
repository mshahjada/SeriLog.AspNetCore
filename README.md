# SeriLog.AspNetCore


<u>Log event:</u>

<ul>
  <li>Verbose - tracing information and debugging minutiae; generally only switched on in unusual situations</li>
  <li>Debug - internal control flow and diagnostic state dumps to facilitate pinpointing of recognised problems</li>
  <li>Information - events of interest or that have relevance to outside observers; the default enabled minimum logging level</li>
  <li>Warning - indicators of possible issues or service/functionality degradation</li>
  <li>Error - indicating a failure within the application or connected system</li>
  <li>Fatal - critical errors causing complete failure of the application</li>
</ul>

<strong>N.B: you can override default provided log event when a http request is made.</strong>


<u>Unusual terms that seri log uses to refer .NET objects map to internal representation:</u>
<ul>
   <li>Stringification: ToSting()</li>
   <li>Destructuring: Taking Complex .NET object and Convert into a structure which can be represented as JSON/XML</li>
   <li>Scalars: .NET Types</li>
</ul>

Showing a list in log:<br/>
Log.Information("Data: {$ListItem}", new { Items: YourCollection })


<p>Covered Area:</p>
<ul>
   <li>Use different Sinks for storing log Like Console, File, RavenDB, Seq</li>
   <li>Configuring serilog from main func/ appsettings</li>
   <li>Custom Enricher creation</li>
   <li>Changing Log level at runtime</li>
</ul>
