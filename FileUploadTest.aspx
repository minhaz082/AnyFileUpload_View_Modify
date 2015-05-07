<%@ Page Title="" Language="C#" MasterPageFile="~/MegnaSolar.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="FileUploadTest.aspx.cs" Inherits="EATL.WebClient.CommonUI.FileUploadTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Save and Download Files from file system</h1>
    <br />
    <div>
        <asp:Label ID="lblwarning" runat="server"></asp:Label><br />
        <asp:FileUpload ID="fileUpload1" runat="server" /><br />
        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" /><br />
    </div>
    <div>
        <asp:GridView ID="gvDetails" CssClass="Gridview" runat="server" AutoGenerateColumns="false"
             AllowPaging="False" OnRowCancelingEdit="gvDetails_RowCancelingEdit" DataKeyNames="IID" CellPadding="4"
                OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating">
                <RowStyle HorizontalAlign="Center" />
                <AlternatingRowStyle HorizontalAlign="Center" />   
            <Columns>                
                <asp:TemplateField HeaderText="Sl." HeaderStyle-Width="30px">
                        <ItemTemplate>
                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>               
                <asp:TemplateField HeaderText="FileName" HeaderStyle-Width="300px">
                    <ItemTemplate>
                        <asp:Label ID="lblFilenAME" runat="server" Text='<%# Eval("FileName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FileLocation" HeaderStyle-Width="300px">
                        <ItemTemplate>
                            <asp:Label ID="lblFilePath" runat="server" Text='<%# Eval("FilePath") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>                            
                            <asp:Label ID="lblFilePath" runat="server" Text='<%# Eval("FilePath") %>'></asp:Label>
                            <asp:FileUpload ID="FileUploadMod" runat="server" />
                        </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View" HeaderStyle-Width="100px" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="lnkDownload_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Change">
                        <ItemTemplate>
                            <asp:LinkButton ID="LkB1" runat="server" CommandName="Edit">Modify</asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LkB2" runat="server" CommandName="Update">Update</asp:LinkButton>
                            <asp:LinkButton ID="LkB3" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>    
</asp:Content>
