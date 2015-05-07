using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using EATL.BLL.Facade;

namespace EATL.WebClient.CommonUI
{
    public partial class FileUploadTest : System.Web.UI.Page
    {
        private SqlConnection con = new SqlConnection(@"Data Source=192.168.1.4;Initial Catalog=MeghnaSolarTestB;User ID=sa;Password=dba@123");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridviewData();
            }
        }
        
        private void BindGridviewData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select IID,FileName,FilePath,CreateBy,CreateDate,UpdateBy,Updatedate,IsRemove from FileUploadForTest", con);
            con.Open();
            da.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                gvDetails.DataSource = dt;
                gvDetails.DataBind();
            }
        }
        // Save files to Folder and files path in database
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload1.PostedFile.FileName != "")
            {
                lblwarning.Text = "";
                string filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
                fileUpload1.SaveAs(Server.MapPath("Files/" + filename));
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into FileUploadForTest(FileName,FilePath,CreateBy,CreateDate,UpdateBy,Updatedate,IsRemove) values(@FileName,@FilePath,@CreateBy,@CreateDate,@UpdateBy,@Updatedate,@IsRemove)", con);
                cmd.Parameters.AddWithValue("@FileName", filename);
                cmd.Parameters.AddWithValue("@FilePath", "Files/" + filename);
                cmd.Parameters.AddWithValue("@CreateBy", Convert.ToInt64(Session["UserID"]));
                cmd.Parameters.AddWithValue("@CreateDate", GeneralConfigurationFacade.GetServerDateTime());
                cmd.Parameters.AddWithValue("@UpdateBy", Convert.ToInt64(Session["UserID"]));
                cmd.Parameters.AddWithValue("@Updatedate", GeneralConfigurationFacade.GetServerDateTime());
                cmd.Parameters.AddWithValue("@IsRemove", "False");

                cmd.ExecuteNonQuery();
                con.Close();
                BindGridviewData();
            }
        }
        // This button click event is used to download files from gridview
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;           
            Label Path = (Label)gvDetails.Rows[gvrow.RowIndex].FindControl("lblFilePath");
            string filePath = Path.Text;
            Response.ContentType = "image/jpg";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
            Response.TransmitFile(Server.MapPath(filePath));            
            Response.End();
            lblwarning.Text = "";
        }        
        //edit event
        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            BindGridviewData();
            lblwarning.Text = "";
        }
        // update event
        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string FileId;
            string FileName;
            string FilePath;
            FileId = gvDetails.DataKeys[e.RowIndex].Value.ToString();
            FileUpload FileUploadMod = (FileUpload)gvDetails.Rows[e.RowIndex].FindControl("FileUploadMod");           
            //FileName = FileUploadMod.PostedFile.FileName;
            //FilePath = "Files/" + FileUploadMod.PostedFile.FileName;
            if (FileUploadMod.HasFile)
            {
                lblwarning.Text = "";
                FileName = FileUploadMod.PostedFile.FileName;
                FilePath = "Files/" + FileUploadMod.PostedFile.FileName;
                FileUploadMod.SaveAs(Server.MapPath("Files/" + FileUploadMod.FileName));

            }
            else
            {
                Label Path = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblFilePath");
                FilePath = Path.Text;
                Label Name = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblFilenAME");
                FileName = Name.Text;
            }

            SqlCommand cmd = new SqlCommand("update FileUploadForTest set FileName='" + FileName + "', FilePath='" + FilePath + "' where IID=" + FileId + "", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            gvDetails.EditIndex = -1;
            BindGridviewData();

        }
        // cancel edit event
        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            BindGridviewData();
            lblwarning.Text = "";
        }
    }
}