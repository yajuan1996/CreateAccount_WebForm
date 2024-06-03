<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="WebForm.Member" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
    <script>
        $(document).ready(function () {
            //日期選擇器
            $('.datepicker').datepicker({
                format: 'yyyy-mm-dd',
                autoclose: false,
                todayHighlight: true
            });
        });
    </script>
    <main aria-labelledby="title">
        
        <div>
            <asp:FormView ID="fmvMember" runat="server" DataKeyNames="Id" DefaultMode="Insert" OnItemInserting="fmvMember_ItemInserting" OnItemUpdating="fmvMember_ItemUpdating">
            
                <InsertItemTemplate>
                    <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("Id") %>' />
                    姓名：<asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox><br/>
                    年齡：<asp:TextBox ID="txtAge" runat="server" Text='<%# Bind("Age") %>'></asp:TextBox><br/>
                    生日：<asp:TextBox ID="txtBirthday" runat="server" Text='<%# Bind("Birthday") %>' CssClass="datepicker"></asp:TextBox><br/>
                    <asp:Button ID="btnInsert" runat="server" Text="建立帳號" CommandName="Insert" />
                </InsertItemTemplate>
            
                <EditItemTemplate>
                    <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("Id") %>' />
                    姓名：<asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox><br/>
                    年齡：<asp:TextBox ID="txtAge" runat="server" Text='<%# Bind("Age") %>'></asp:TextBox><br/>
                    生日：<asp:TextBox ID="txtBirthday" runat="server" Text='<%# Bind("Birthday") %>' CssClass="datepicker"></asp:TextBox><br/>
                    <asp:Button ID="btnUpdate" runat="server" Text="修改帳號" CommandName="Update" />
                </EditItemTemplate>
            </asp:FormView>
            <br/>
            <asp:GridView ID="gdvMember" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table-bordered" OnRowCommand="gdvMember_RowCommand" OnRowDeleting="gdvMember_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" Visible="false" SortExpression="Id" />
                    <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="Name" />
                    <asp:BoundField DataField="Age" HeaderText="年齡" SortExpression="Age" />
                    <asp:BoundField DataField="Birthday" HeaderText="生日" SortExpression="Birthday" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="修改" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>' />
                            <asp:Button ID="btnDelete" runat="server" Text="刪除" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        
        </div>
        
    </main>
    
</asp:Content>
