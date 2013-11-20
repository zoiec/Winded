<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .dropCap li:first-letter
        {
            display: inline-block;
            font-size: 1.25em;
            color: darkgreen;
            padding-right:2px;
        }
    </style>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <article>
        <h2>Ad-Hoc Demo Notes</h2>
        <p>
            This is a rapid prototype example to illustrate how to use Entity Framework on a database without stored procedures. Based on the <a href="https://northwinddatabase.codeplex.com/">Northwind Traders database</a>.
        </p>
        <p><strong>LINQ</strong> (<em>Languge-INtegrated-Query</em>) provides a powerful and declarative approach to manipulating (filtering and re-shaping) data. The <strong>Entity Framework</strong> (<em>EF</em>) provides the &quot;plumbing&quot; that 1) dynamically generates SQL statements to run against a database and 2) map the results of SQL statements to OOP-based classes (whether C#, VB, or any other .NET supported language). <strong><em>LINQ-to-Entities</em></strong> is a merger of Linq and EF &mdash; an OOP-based (C#, VB, etc.) way to easily create complex SQL statements in order to manipulate database information and generate rich complex objects.</p>
        <h2>The Benefits of LINQ-to-Entities</h2>
        <p>Because of the power of LINQ-to-Entities, the role and importance of database stored procedures (or <em>sprocs</em>) is greatly diminished. And that is a <em>good</em> thing. In the first case, it helps reduce what is typically an over-abundance of stored procedures. This reduced need for stored procedures provides additional, more powerful, advantages:</p>
        <ul>
            <li><strong>Improved maintainability</strong> &ndash; Less effort is required to adjust or &quot;re-shape&quot; data for new business requirements.</li>
            <li><strong>Greater flexibility</strong> &ndash; New business requirements result in changes to the application only, not the application <em>and</em> the database.</li>
            <li><strong>Reduced Impact</strong> – Changes to the database tend to &quot;ripple&quot; upward through the rest of the application, requiring changes &quot;everywhere&quot;</li>
            <li><strong>Reduced costs</strong> &ndash; All of the above points save money, since it&#39;s cheaper &amp; easier to both implement and test changes made using LINQ-to-Entities.</li>
        </ul>
        <h3>A bit of History</h3>
        <p>Because SQL is such an efficient and powerful way of re-shaping and processing database information, stored procedures were, historically, the de-facto way to allow real-world applications to interact with the database. But, it came at a price:</p>
        <p>Stored procedures effectively became the means to perform everything from basic CRUD operations on any given database table to generating complex and specific report summaries. As a result, a huge number of stored procedures are often needed in a database. In fact, the number of sprocs vastly outnumbers the tables in a typical database. For example, it&#39;s not unusual to see 5+ stored procedures per database table for simple CRUD operations.</p>
        <ul class="dropCap">
            <li><strong>Create</strong> a row of data (INSERT)</li>
            <li><strong>Read</strong> all the rows of a table</li>
            <li><strong>Read</strong> a single row in a table (by primary key)</li>
            <li><strong>Read</strong> rows based on some filter, such as a foreign key value (one per foriegn key)</li>
            <li><strong>Update</strong> one or more rows (UPDATE)</li>
            <li><strong>Delete</strong> one or more rows (DELETE)</li>
        </ul>
        <p>Ultimately, however, these stored procedures only exist to serve the external business application that&#39;s trying to access the database data. Because it&#39;s nigh impossible to predict the number of places any given sproc might be used in an application, the impact of <em>changing</em> a sproc is fraught with potential &quot;run-time failures&quot;. Instead, what&#39;s typically done is that the business needs are met by creating additional stored procedures rather than changing existing ones. This results in 1) a sense of &quot;fragmentation&quot; regarding the re-usability/suitability of the original sprocs, as well as 2) a sense of &quot;clutter&quot; in the whole realm of a database&#39;s stored procedures.</p>
    </article>

    <aside>
        <h3>Demo Roadmap</h3>
        <p>        
            A few features will be demonstrated in this sample.
        </p>
        <ul>
            <li><asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" Checked="true" />
                Nested GridView to display Object Graph details of an Entity
            </li>
            <li><asp:CheckBox ID="CheckBox2" runat="server" Enabled="false" Checked="true" />
                Display images from byte[] in database
            </li>
            <li><asp:CheckBox ID="CheckBox3" runat="server" Enabled="false" Checked="false" />
                Transactional processing in class library using Linq
            </li>
            <li><asp:CheckBox ID="CheckBox4" runat="server" Enabled="false" Checked="false" />
                TBA
            </li>
            <li><asp:CheckBox ID="CheckBox5" runat="server" Enabled="false" Checked="false" />
                TBA
            </li>
        </ul>
    </aside>
</asp:Content>