# RescueHub – Hệ thống cứu hộ cứu nạn (.NET 8 MVC)

## Cách chạy:
1. Chỉnh `appsettings.json` connection string
2. Chạy migration:
```
dotnet ef migrations add Init
dotnet ef database update
```
3. Run:
```
dotnet run
```

## Tài khoản mặc định:
- Admin: admin@rescuehub.local / Admin@123
- Rescuer: rescue1@rescuehub.local / Rescue@123

## Tính năng:
- Người dân gửi yêu cầu + GPS + ảnh
- Rescuer xem Dashboard + Map + nhận nhiệm vụ
- Admin xem thống kê + map tổng + export CSV
