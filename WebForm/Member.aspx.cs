using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm
{
    public partial class Member : System.Web.UI.Page
    {
        // 會員
        public class clsMember
        {            
            /// <summary>
            /// 編號
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 姓名
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 年齡
            /// </summary>
            public int Age { get; set; }

            /// <summary>
            /// 生日
            /// </summary>
            public string Birthday { get; set; }
        }

        // 會員資料List
        private static List<clsMember> members = new List<clsMember>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                gdvMemberBind();
            }
        }

        //綁定GridView
        private void gdvMemberBind()
        {
            gdvMember.DataSource = members;
            gdvMember.DataBind();
        }

        //指定選取列
        protected void gdvMember_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gdvMember.Rows[rowIndex];

            if (e.CommandName == "Select")
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Age", typeof(string));
                dt.Columns.Add("Birthday", typeof(string));

                DataRow dr = dt.NewRow();
                dr["Id"] = gdvMember.DataKeys[rowIndex]["Id"];
                dr["Name"] = row.Cells[1].Text;
                dr["Age"] = row.Cells[2].Text;
                dr["Birthday"] = row.Cells[3].Text;
                dt.Rows.Add(dr);

                fmvMember.ChangeMode(FormViewMode.Edit);
                fmvMember.DataSource = dt;
                fmvMember.DataBind();

            }
        }        

        //刪除
        protected void gdvMember_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {            
            int id = Convert.ToInt32(gdvMember.DataKeys[e.RowIndex].Value);
            clsMember member = members.Find(x => x.Id == id);
            members.Remove(member);
            gdvMemberBind();
        }
        

        //建立帳號
        protected void fmvMember_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            // 取得新增資料
            TextBox txtName = (TextBox)fmvMember.FindControl("txtName");
            TextBox txtAge = (TextBox)fmvMember.FindControl("txtAge");
            TextBox txtBirthday = (TextBox)fmvMember.FindControl("txtBirthday");

            // 新增
            clsMember member = new clsMember();
            member.Id = members.Count == 0 ? 1: members.Max(x => x.Id) + 1;
            member.Name = txtName.Text;
            member.Age = Convert.ToInt32(txtAge.Text);
            member.Birthday = txtBirthday.Text;
            members.Add(member);

            gdvMemberBind();
            
            //新增完畢後清空欄位
            fmvMemberClear();
        }

        //清空欄位
        protected void fmvMemberClear()
        {
            TextBox txtName = (TextBox)fmvMember.FindControl("txtName");
            TextBox txtAge = (TextBox)fmvMember.FindControl("txtAge");
            TextBox txtBirthday = (TextBox)fmvMember.FindControl("txtBirthday");

            txtName.Text = "";
            txtAge.Text = "";
            txtBirthday.Text = "";
        }

        //修改帳號
        protected void fmvMember_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            // 取得修改資料
            HiddenField hdnId = (HiddenField)fmvMember.FindControl("hdnId");
            TextBox txtName = (TextBox)fmvMember.FindControl("txtName");
            TextBox txtAge = (TextBox)fmvMember.FindControl("txtAge");
            TextBox txtBirthday = (TextBox)fmvMember.FindControl("txtBirthday");

            int id = Convert.ToInt32(hdnId.Value);
            clsMember updMember = members.Find(x => x.Id == id);
            updMember.Name = txtName.Text;
            updMember.Age = Convert.ToInt32(txtAge.Text);
            updMember.Birthday = txtBirthday.Text;

            gdvMemberBind();

            //清空欄位
            fmvMember.ChangeMode(FormViewMode.Insert);
        }

    }
}