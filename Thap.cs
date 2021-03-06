using System;
using System.Drawing;
using System.Windows.Forms;

class Thap : UserControl
{
    public Dia[] CacDia;
    private Cot CayCot;

    public Thap()
    {
        this.Width = 260;
        this.Height = 350;
        this.BackColor = Color.Black;

        this.CacDia = new Dia[0];

        this.CayCot = new Cot(300);
        this.CayCot.Location = new Point(120,0);
        this.Controls.Add(this.CayCot);
    }

    public static Thap TaoKhoiDau(int sodia)
    {
        Thap kq = new Thap();
        kq.CacDia = new Dia[sodia];
        for (int i = 0; i < kq.CacDia.Length; i++)
        {
            kq.CacDia[i] = new Dia(i);
            kq.CacDia[i].Location = new Point(100-i*5, 300 - (sodia - i) * 15);
            kq.Controls.Add(kq.CacDia[i]);
        }
        kq.Controls.Add(kq.CayCot);
        return kq;
    }

    public static Thap TaoKhoiDau(Dia diadau)
    {
        Thap kq = new Thap();
        kq.CacDia = new Dia[diadau.So+1];
        for (int i = 0; i < kq.CacDia.Length; i++)
        {
            kq.CacDia[i] = new Dia(i);
            kq.CacDia[i].Location = new Point(100 - i * 5, 300 - (diadau.So - i) * 15);
            kq.Controls.Add(kq.CacDia[i]);
        }
        kq.Controls.Add(kq.CayCot);
        return kq;
    }

    //Thực hiện cho đi đĩa đầu tiên
    public void ChoDia()
    {
        //Tạo 1 Tháp mới với chiều cao giảm đí 1
        Thap moi = new Thap();
        moi.CacDia = new Dia[this.CacDia.Length - 1];
        //Huỷ các đĩa hiện tại ra khỏi Tháp cũ
        for (int i = 0; i < this.CacDia.Length; i++)
        {
            this.Controls.Remove(this.CacDia[i]);
        }
        //Chuyển các đĩa của Tháp cũ qua Tháp mới(không có đĩa đầu tiên)
        for (int i = 0; i < moi.CacDia.Length; i++)
        {
            moi.CacDia[i] = new Dia(this.CacDia[i + 1].So);
            moi.CacDia[i].Location = this.CacDia[i + 1].Location;
        }

        //Thay Tháp cũ bằng Tháp mới
        this.CacDia = moi.CacDia;
        for (int i = 0; i < this.CacDia.Length; i++)
        {
            this.Controls.Add(this.CacDia[i]);
        }
        this.Controls.Add(this.CayCot);
    }


    //Thực hiện nhận 1 đĩa và thâm vào chồng đĩa của mình nếu được
    public bool NhanDia(Dia dia)
    {
        //Kiểm tra có nhận được đĩa đưa vào hay không
        if (this.CacDia.Length == 0 || this.CacDia[0].Width > dia.Width)
        {
            //Tạo Tháp mới với chiều cao tăng 1.
            Dia[] moi = new Dia[this.CacDia.Length + 1];
            //Đĩa đầu tiên của Tháp mới là đĩa nhận vào
            moi[0] =new Dia(dia.So);
            moi[0].Location = new Point((this.Width - moi[0].Width) / 2 - 3, 300 - (moi.Length - 0) * 15);
            //Các đĩa tiếp theo là toàn bộ đĩa của Tháp cũ
            for (int i = 1; i < moi.Length; i++)
            {
                moi[i] = this.CacDia[i - 1];
                moi[i].Location = new Point((this.Width - moi[i].Width) / 2 - 3, 300 - (moi.Length - i) * 15);
            }
            //Thay Tháp cũ bằng Tháp mới
            this.CacDia = moi;
            for (int i = 0; i < this.CacDia.Length; i++)
            {
                this.Controls.Add(this.CacDia[i]);
            }
            this.Controls.Add(this.CayCot);
            //Chuyển đĩa thành công
            return true;
        }
        //Chuyển đĩa thất bại
        else
            return false;
    }

    
    //Tính số đĩa đúng vị trí so với Tháp khởi đầu
    public int SoDiaDung(int sodia)
    {
        //Tạo 1 Tháp tương đương Tháp khởi đầu để so sánh
        Thap tam = Thap.TaoKhoiDau(sodia);
        int kq = 0;
        //Kiểm tra từ dưới lên
        int k = tam.CacDia.Length-1;
        for (int i = this.CacDia.Length - 1; i >= 0; i--)
        {
                //Nếu đĩa đúng vị trí thì đếm được thêm 1 đĩa
                if (this.CacDia[i].Width == tam.CacDia[k--].Width)
                    kq++;
                //Ngược lại dừng
                else
                    break;
        }
        return kq;
    }
    //Tính chi phí H
    public int SoLanItNhat(int sodia)
    {
        int h;
        h=sodia + this.CacDia.Length - 2 * SoDiaDung(sodia);
        return h;
    }

}
