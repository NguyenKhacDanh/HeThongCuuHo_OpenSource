# RescueHub – Hệ thống cứu hộ cứu nạn (.NET 8 MVC)

Nguồn phát triển & ý tưởng ban đầu:
→ **Nguyễn Khắc Danh**

Mục đích dự án:
Hỗ trợ tiếp nhận – điều phối – thống kê cứu hộ trong thiên tai, lũ lụt.
Dự án hiện chưa hoàn thiện và được công bố dạng open-source để cộng đồng cùng phát triển tiếp.

---

## 🚀 Mục tiêu của dự án

- Tạo một hệ thống nhanh – dễ dùng – miễn phí – open source phục vụ các đội cứu hộ & người dân.
- Có thể triển khai thực tế cho:
  • Chính quyền địa phương  
  • Đội cứu hộ tình nguyện  
  • Cộng đồng ứng phó thiên tai  
  • Các nhóm xã hội hỗ trợ vùng ngập lụt / sạt lở

---

## 🛠 Công nghệ sử dụng

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core – Code First
- SQL Server
- ASP.NET Identity
- Bootstrap 5 (Dark / Light Mode)
- Leaflet Map + OpenStreetMap
- HTML5 Geolocation API
- Upload hình ảnh hiện trường

---

## 📦 Chức năng đã hoàn thiện (v1)

### 👤 Người dân (Anonymous – không cần login)
- Gửi yêu cầu cứu hộ khẩn cấp.
- Lấy GPS tự động.
- Upload ảnh hiện trường.
- Nhập mô tả chi tiết + yếu tố nguy hiểm.
- Sinh mã yêu cầu REQ-yyyyMMdd-xxxx.
- Gửi báo cáo "Tôi đang an toàn".

### 🚑 Rescuer (Người cứu hộ)
- Đăng ký Rescuer (khu vực hoạt động + bán kính).
- Dashboard xem yêu cầu gần nhất theo mức độ.
- Tính khoảng cách đến điểm yêu cầu.
- Map cứu hộ bằng Leaflet.
- Nhận nhiệm vụ → đang cứu hộ → hoàn thành / không thể tiếp cận.
- Log notification (giả lập SMS/Email).

### 🛠 Admin Panel
- Dashboard thống kê theo trạng thái.
- Số yêu cầu mới (1 giờ / 24 giờ).
- Bản đồ tổng hợp request / safe report / shelter.
- Quản lý rescuer.
- Export CSV / báo cáo.

---

## 🔮 Định hướng phát triển (Open Source)

### Dự kiến v2:
- Realtime Map bằng SignalR.
- Push Notification (Firebase / OneSignal).
- Mobile App (Flutter).
- Tích hợp cảnh báo Zalo OA / SMS Gateway.
- Tối ưu UI cho vùng Internet yếu.
- PWA Offline mode.
- Phân tuyến nhiệm vụ nâng cao theo khu vực.

---

## 🧪 Cách chạy dự án

1. Cấu hình connection string trong `appsettings.json`  
2. Chạy migration:  
dotnet ef migrations add Init  
dotnet ef database update  
3. Chạy web:  
dotnet run

---

## 🔐 Tài khoản mặc định (được seed sẵn)

Admin:  
• Email: admin@rescuehub.local  
• Password: Admin@123  

Rescuer:  
• Email: rescue1@rescuehub.local  
• Password: Rescue@123

---

## ❤️ Ghi chú tác giả

Dự án này được tạo ra nhằm hỗ trợ cộng đồng trong mùa lũ và thiên tai.
Mong rằng nó sẽ giúp ích cho các đội cứu hộ – hoặc làm nền tảng để xây dựng hệ thống lớn hơn.

Dự án hoàn toàn open-source. Mọi đóng góp đều được hoan nghênh.

— RescueHub by Nguyễn Khắc Danh - 0981494148

---

# RescueHub – Disaster Rescue Management System (.NET 8 MVC) - US

Original concept & development:  
→ **Nguyen Khac Danh**

Purpose of the project:  
To support receiving, coordinating, and managing rescue operations during natural disasters and floods.  
This project is not fully completed and is released as **open-source** for community contributions.

---

## 🚀 Project Goals

- Build a fast, easy-to-use, open-source system for rescue teams and civilians.
- Applicable for:
  • Local authorities  
  • Volunteer rescue teams  
  • Disaster-response communities  
  • Organizations supporting flooded regions

---

## 🛠 Technologies Used

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core – Code First
- SQL Server
- ASP.NET Identity
- Bootstrap 5 (Dark / Light Theme)
- Leaflet Map + OpenStreetMap
- HTML5 Geolocation API
- Image upload system

---

## 📦 Completed Features (v1)

### 👤 Civilians (Anonymous – No Login Required)
- Submit emergency rescue requests.
- Auto GPS detection.
- Upload incident photos.
- Detailed hazard description.
- Auto-generated request code: REQ-yyyyMMdd-xxxx.
- Submit "I'm Safe" reports.

### 🚑 Rescuer
- Register with operating area + radius.
- Dashboard showing nearby prioritized requests.
- Distance calculation to incidents.
- Leaflet rescue map.
- Accept → In-progress → Completed / Cannot Reach workflow.
- Notification logs (simulated SMS/Email).

### 🛠 Admin Panel
- Dashboard with status statistics.
- New request count (1h / 24h).
- Unified map for all request types.
- Manage rescuers.
- CSV export and reporting.

---

## 🔮 Future Roadmap (Open Source)

Planned for v2:
- Realtime Map (SignalR)
- Push Notification (Firebase / OneSignal)
- Mobile App (Flutter)
- Zalo OA / SMS Gateway Integration
- Low-connectivity UI optimizations
- PWA offline mode
- Advanced region-based mission routing

---

## 🧪 How to run

1. Configure connection string  
2. Run migrations  
3. Run the web app

---

## Default accounts

Admin:  
Email: admin@rescuehub.local  
Password: Admin@123  

Rescuer:  
Email: rescue1@rescuehub.local  
Password: Rescue@123

---

## ❤️ Author Notes

This open-source project is created to support communities during flood seasons and natural disasters.  
Contributions and enhancements from the community are welcome.

— RescueHub by Nguyen Khac Danh - 0981494148
