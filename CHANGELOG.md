Semua perubahan penting pada project ini akan dicatat di file ini. 
[1.0.0] - 2026-02-09
- Yang ditambahkan
1. Database: Implementasi SQLite sebagai database utama.
2. Seeder: Penambahan data awal otomatis untuk tabel Customers (termasuk Mauren & Fazel).
3. CRUD: Implementasi lengkap Operasi Create, Read, Update, dan Delete pada CustomersController.
4. Soft Delete: Fitur penghapusan data secara logis menggunakan kolom DeletedAt.
5. Validation: Validasi input pada API untuk memastikan integritas data.
6. Documentation: Implementasi Swagger UI dan pembaruan README.md.

- Yang sudah diubah
1. Mengalihkan provider database dari SQL Server ke SQLite untuk kemudahan pengembangan lokal.
2. Memperbarui struktur routing API menjadi versi 1 (api/v1/customers).