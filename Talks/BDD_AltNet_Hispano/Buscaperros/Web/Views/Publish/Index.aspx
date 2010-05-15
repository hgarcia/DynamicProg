<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Domain.Model.Pet>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Published pets.
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Published pets.</h2>

    <table id="list">
        <tr>
            <th>
                Name
            </th>
            <th>
                Age
            </th>
            <th>
                Breed
            </th>
            <th>
                Status
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.Age) %>
            </td>
            <td>
                <%= Html.Encode(item.Breed.Name) %>
            </td>
            <td>
            <%= Html.Encode(item.Status.Name) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

