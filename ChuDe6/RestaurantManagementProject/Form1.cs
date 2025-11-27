using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogic;
using DataAccess;
using DataAccess.OL;

namespace RestaurantManagementProject
{
    public partial class frmFood : Form
    {
        public bool TablesChanged { get; private set; } = false;
        public bool FoodsChanged { get; private set; } = false;

        private List<Category> listCategory = new List<Category>();
        private List<Food> listFood = new List<Food>();
        private Food foodCurrent = new Food();

        public frmFood()
        {
            InitializeComponent();
        }

        private void frmFood_Load(object sender, EventArgs e)
        {
            LoadCategory();
            LoadFoodDataToListView();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtUnit.Clear();
            txtPrice.Clear();
            txtNotes.Clear();
            cbbCategory.SelectedIndex = -1;

        }


        private void LoadCategory()
        {
            //Gọi đối tượng CategoryBL từ tầng BusinessLogic
            CategoryBL categoryBL = new CategoryBL();
            // Lấy dữ liệu gán cho biến toàn cục listCategory
            listCategory = categoryBL.GetAll();
            // Chuyển vào Combobox với dữ liệu là ID, hiển thị là Name
            cbbCategory.DataSource = listCategory;
            cbbCategory.ValueMember = "ID";
            cbbCategory.DisplayMember = "Name";
        }



        public void LoadFoodDataToListView()
        {
            //Gọi đối tượng FoodBL từ tầng BusinessLogic
            FoodBL foodBL = new FoodBL();
            // Lấy dữ liệu
            listFood = foodBL.GetAll();
            int count = 1; // Biến số thứ tự
                           // Xoá dữ liệu trong ListView
            lsvFood.Items.Clear();
            // Duyệt mảng dữ liệu để đưa vào ListView
            foreach (var food in listFood)
            {
                // số thứ tự
                ListViewItem item = lsvFood.Items.Add(count.ToString());
                // Đưa dữ liệu Name, Unit, price vào cột tiếp theo
                item.SubItems.Add(food.Name);
                item.SubItems.Add(food.Unit);
                item.SubItems.Add(food.Price.ToString());
                // Theo dữ liệu của bảng Category ID, lấy Name để hiển thị
                string foodName = listCategory.Find(x => x.ID == food.FoodCategoryID).Name;
                item.SubItems.Add(foodName);
                // Đưa dữ liệu Notes vào cột cuối
                item.SubItems.Add(food.Notes);
                count++;
            }
        }


        private void lsvFood_Click(object sender, EventArgs e)
        {
            // Duyệt toàn bộ dữ liệu trong ListView
            for (int i = 0; i < lsvFood.Items.Count; i++)
            {
                // Nếu có dòng được chọn thì lấy dòng đó
                if (lsvFood.Items[i].Selected)
                {
                    // Lấy các tham số và gán dữ liệu vào các ô
                    foodCurrent = listFood[i];
                    txtName.Text = foodCurrent.Name;
                    txtUnit.Text = foodCurrent.Unit;
                    txtPrice.Text = foodCurrent.Price.ToString();
                    txtNotes.Text = foodCurrent.Notes;
                    // Lấy index của Combobox theo FoodCategoryID
                    cbbCategory.SelectedIndex = listCategory.FindIndex(x => x.ID == foodCurrent.FoodCategoryID);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Gọi phương thức thêm dữ liệu
            int result = InsertFood();
            if (result >= 0) // Nếu thêm thành công
            {
                // Thông báo kết quả
                MessageBox.Show("Thêm dữ liệu thành công!");
                // Tải lại dữ liệu cho ListView
                LoadFoodDataToListView();
            }
            else
                MessageBox.Show("Thêm dữ liệu không thành công. Vui lòng kiểm tra lại dữ liệu nhập!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Hỏi người dùng có chắc chắn xóa hay không?
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa mẫu tin này?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                // Khai báo đối tượng FoodBL từ BusinessLogic
                FoodBL foodBL = new FoodBL();

                if (foodBL.Delete(foodCurrent) >= 0) // Nếu xóa thành công
                {
                    MessageBox.Show("Xóa thực phẩm thành công!");
                    // Tải lại dữ liệu cho ListView
                    LoadFoodDataToListView();
                }
                else
                    MessageBox.Show("Xóa không thành công!");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Gọi phương thức cập nhật dữ liệu
            int result = UpdateFood();
            if (result >= 0) // Nếu cập nhật thành công
            {
                // Thông báo kết quả
                MessageBox.Show("Cập nhật dữ liệu thành công!");
                // Tải lại dữ liệu cho ListView
                LoadFoodDataToListView();
            }
            else
                MessageBox.Show("Cập nhật dữ liệu không thành công. Vui lòng kiểm tra lại dữ liệu nhập!");
        }


        public int UpdateFood()
        {
            // Khai báo đối tượng Food và lấy đối tượng hiện hành
            Food food = foodCurrent;

            // Kiểm tra nếu ô nhập khác rỗng
            if (txtName.Text == "" || txtUnit.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Chưa nhập đủ dữ liệu cho các ô, vui lòng nhập lại!");
            }
            else
            {
                // Nhận giá trị Name, Unit, và Notes khi người dùng sửa
                food.Name = txtName.Text;
                food.Unit = txtUnit.Text;
                food.Notes = txtNotes.Text;

                // Giá trị price là số nên cần bắt lỗi khi người dùng nhập sai
                int price = 0;
                try
                {
                    // Chuyển giá trị từ kiểu văn bản qua kiểu int
                    price = int.Parse(txtPrice.Text);
                }
                catch
                {
                    // Nếu sai, gán giá = 0
                    price = 0;
                }

                food.Price = price;
                // Giá trị FoodCategoryID được lấy từ ComboBox
                food.FoodCategoryID = int.Parse(cbbCategory.SelectedValue.ToString());

                // Khai báo đối tượng FoodBL từ tầng Business
                FoodBL foodBL = new FoodBL();

                // Cập nhật dữ liệu trong bảng
                return foodBL.Update(food);
            }
            return -1;
        }

        public int InsertFood()
        {
            // Khai báo đối tượng Food từ tầng DataAccess
            Food food = new Food();
            food.ID = 0;

            // Kiểm tra nếu các ô nhập khác rỗng
            if (txtName.Text == "" || txtUnit.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Chưa nhập đủ dữ liệu cho các ô, vui lòng nhập lại!");
            }
            else
            {
                // Nhận giá trị Name, Unit, và Notes từ người dùng nhập vào
                food.Name = txtName.Text;
                food.Unit = txtUnit.Text;
                food.Notes = txtNotes.Text;

                // Giá trị price là số nên cần bắt lỗi khi người dùng nhập sai
                int price = 0;
                try
                {
                    // Cố gắng lấy giá trị
                    price = int.Parse(txtPrice.Text);
                }
                catch
                {
                    // Nếu sai, gán giá = 0
                    price = 0;
                }

                food.Price = price;
                // Giá trị FoodCategoryID được lấy từ ComboBox
                food.FoodCategoryID = int.Parse(cbbCategory.SelectedValue.ToString());

                // Khai báo đối tượng FoodBL từ tầng Business
                FoodBL foodBL = new FoodBL();

                // Chèn dữ liệu vào bảng
                return foodBL.Insert(food);
            }
            return -1;
        }

        private void btnThemBan_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtThemBan.Text.Trim(), out int soBan) || soBan <= 0)
            {
                MessageBox.Show("Số bàn phải là số nguyên > 0.", "Lỗi nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var banBL = new BanBL();
            int added = 0;

            for (int i = 0; i < soBan; i++)
            {
                // 1) Insert để lấy ID mới (tạm để Name = null)
                var b = new Ban { ID = 0, Name = null, Status = 0, Capacity = 4 };
                int newId = banBL.Insert(b);              // DA sẽ trả về ID mới (OUTPUT @ID)

                if (newId > 0)
                {
                    // 2) Update lại Name = "Bàn {ID}" theo đúng ID thực tế
                    b.ID = newId;
                    b.Name = "Bàn " + newId;
                    banBL.Update(b);
                    added++;
                }
            }

            MessageBox.Show(added > 0 ? $"Đã thêm {added} bàn!" : "Không thêm được bàn nào!",
                "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtThemBan.Clear();

            // 🔔 Thông báo cho form cha reload layout bàn
            this.TablesChanged = this.TablesChanged || (added > 0);
        }


        private void txtThemBan_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
