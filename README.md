# Survivor Web API

Survivor Web API, Survivor uygulaması için geliştirilmiş bir RESTful API'dir. Bu API, kategoriler (`Category`) ve yarışmacılar (`Competitor`) arasında bire-çok (one-to-many) ilişkisi kurarak veri yönetimi sağlar. Kullanıcılar, kategoriler oluşturabilir, yarışmacıları ekleyebilir, güncelleyebilir ve silebilirler. Ayrıca, mevcut verileri sorgulayarak kategorilere ve yarışmacılara ait bilgileri görüntüleyebilirler.

## İçindekiler

- [Özellikler](#özellikler)
- [Teknolojiler](#teknolojiler)
- [Kurulum](#kurulum)
- [Kullanım](#kullanım)
  - [Kategori Yönetimi](#kategori-yönetimi)
  - [Yarışmacı Yönetimi](#yarışmacı-yönetimi)
- [API Dökümantasyonu](#api-dökümantasyonu)


## Özellikler

- **Kategori Yönetimi:** Kategoriler oluşturma, listeleme, detaylarını görüntüleme.
- **Yarışmacı Yönetimi:** Yarışmacılar oluşturma, güncelleme, silme, listeleme ve detaylarını görüntüleme.
- **Veri Doğrulama:** Girdi verilerinin doğrulanması ve hataların yönetimi.
- **İlişkisel Veri Yapısı:** Kategoriler ile yarışmacılar arasında bire-çok ilişkisi.
- **Swagger Dökümantasyonu:** API'nin etkileşimli dökümantasyonu için Swagger kullanımı.

## Teknolojiler

- **Programlama Dili:** C#
- **Framework:** ASP.NET Core
- **Veritabanı:** Entity Framework Core (SQL Server)
- **API Belgeleri:** Swagger (Swashbuckle)
- **Diğer:** AutoMapper (isteğe bağlı)

## Kurulum

### Ön Koşullar

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) veya üzeri
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (veya tercih ettiğiniz başka bir veritabanı)
- [Git](https://git-scm.com/downloads)

### Adımlar

1. **Depoyu Klonlayın:**

    ```bash
    git clone https://github.com/kullanici_adiniz/survivor-web-api.git
    cd survivor-web-api
    ```

2. **Bağımlılıkları Yükleyin:**

    ```bash
    dotnet restore
    ```

3. **Veritabanı Bağlantı Dizesini Ayarlayın:**

    `appsettings.json` dosyasını açın ve `ConnectionStrings` bölümünü kendi veritabanı bağlantı dizesi ile güncelleyin.

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=SurvivorDB;Trusted_Connection=True;MultipleActiveResultSets=true"
      },
      // Diğer ayarlar...
    }
    ```

4. **Veritabanı Migrasyonlarını Uygulayın:**

    ```bash
    dotnet ef database update
    ```

5. **Uygulamayı Başlatın:**

    ```bash
    dotnet run
    ```

6. **Swagger UI'ye Erişin:**

    Uygulama çalıştıktan sonra, tarayıcınızda `https://localhost:5001/swagger` adresine giderek API dökümantasyonunu görüntüleyebilirsiniz.

## Kullanım

### Kategori Yönetimi

#### Kategori Oluşturma

- **Endpoint:** `POST /api/categories`
- **Açıklama:** Yeni bir kategori oluşturur.
- **İstek Gövdesi:**

    ```json
    {
        "name": "Yeni Kategori"
    }
    ```

- **Başarılı Yanıt:**
    - **Durum Kodu:** 201 Created
    - **Yanıt Gövdesi:**

        ```json
        {
            "id": 3,
            "name": "Yeni Kategori",
            "createDate": "2024-10-13T12:34:56Z",
            "modifiedDate": null,
            "isDeleted": false,
            "competitors": []
        }
        ```

#### Kategori Getirme

- **Endpoint:** `GET /api/categories/{id}`
- **Açıklama:** Belirtilen ID'ye sahip kategoriyi ve ilişkili yarışmacıları getirir.
- **Başarılı Yanıt:**
    - **Durum Kodu:** 200 OK
    - **Yanıt Gövdesi:**

        ```json
        {
            "id": 1,
            "name": "Ünlüler",
            "createDate": "2024-01-01T00:00:00Z",
            "modifiedDate": "2024-01-01T00:00:00Z",
            "isDeleted": false,
            "competitors": [
                {
                    "id": 1,
                    "firstName": "Kıvanç",
                    "lastName": "Tatlıtuğ",
                    "categoryId": 1,
                    "createDate": "2024-01-01T00:00:00Z",
                    "modifiedDate": "2024-01-01T00:00:00Z",
                    "isDeleted": false
                },
                // Diğer yarışmacılar...
            ]
        }
        ```

### Yarışmacı Yönetimi

#### Yarışmacı Oluşturma

- **Endpoint:** `POST /api/competitor`
- **Açıklama:** Belirli bir kategoriye yeni bir yarışmacı ekler.
- **İstek Gövdesi:**

    ```json
    {
        "firstName": "Ali",
        "lastName": "Veli",
        "categoryId": 1
    }
    ```

- **Başarılı Yanıt:**
    - **Durum Kodu:** 201 Created
    - **Yanıt Gövdesi:**

        ```json
        {
            "id": 5,
            "firstName": "Ali",
            "lastName": "Veli",
            "categoryId": 1,
            "createDate": "2024-10-13T12:35:00Z",
            "modifiedDate": "2024-10-13T12:35:00Z",
            "isDeleted": false
        }
        ```

#### Yarışmacı Getirme

- **Endpoint:** `GET /api/competitor/{id}`
- **Açıklama:** Belirtilen ID'ye sahip yarışmacıyı ve ilişkili kategorisini getirir.
- **Başarılı Yanıt:**
    - **Durum Kodu:** 200 OK
    - **Yanıt Gövdesi:**

        ```json
        {
            "id": 1,
            "firstName": "Kıvanç",
            "lastName": "Tatlıtuğ",
            "categoryId": 1,
            "createDate": "2024-01-01T00:00:00Z",
            "modifiedDate": "2024-01-01T00:00:00Z",
            "isDeleted": false,
            "category": {
                "id": 1,
                "name": "Ünlüler",
                "createDate": "2024-01-01T00:00:00Z",
                "modifiedDate": "2024-01-01T00:00:00Z",
                "isDeleted": false,
                "competitors": null
            }
        }
        ```

#### Tüm Yarışmacıları Getirme

- **Endpoint:** `GET /api/competitor`
- **Açıklama:** Tüm yarışmacıları listeler.
- **Başarılı Yanıt:**
    - **Durum Kodu:** 200 OK
    - **Yanıt Gövdesi:**

        ```json
        [
            {
                "id": 1,
                "firstName": "Kıvanç",
                "lastName": "Tatlıtuğ",
                "categoryId": 1,
                "createDate": "2024-01-01T00:00:00Z",
                "modifiedDate": "2024-01-01T00:00:00Z",
                "isDeleted": false,
                "category": {
                    "id": 1,
                    "name": "Ünlüler",
                    "createDate": "2024-01-01T00:00:00Z",
                    "modifiedDate": "2024-01-01T00:00:00Z",
                    "isDeleted": false,
                    "competitors": null
                }
            },
            // Diğer yarışmacılar...
        ]
        ```

#### Kategoriye Göre Yarışmacı Getirme

- **Endpoint:** `GET /api/competitor/category/{categoryId}`
- **Açıklama:** Belirtilen kategoriye ait tüm yarışmacıları listeler.
- **Başarılı Yanıt:**
    - **Durum Kodu:** 200 OK
    - **Yanıt Gövdesi:**

        ```json
        [
            {
                "id": 1,
                "firstName": "Kıvanç",
                "lastName": "Tatlıtuğ",
                "categoryId": 1,
                "createDate": "2024-01-01T00:00:00Z",
                "modifiedDate": "2024-01-01T00:00:00Z",
                "isDeleted": false,
                "category": null
            },
            {
                "id": 2,
                "firstName": "Tuğba",
                "lastName": "Büyüküstün",
                "categoryId": 1,
                "createDate": "2024-01-01T00:00:00Z",
                "modifiedDate": "2024-01-01T00:00:00Z",
                "isDeleted": false,
                "category": null
            }
            // Diğer yarışmacılar...
        ]
        ```

#### Yarışmacı Güncelleme

- **Endpoint:** `PUT /api/competitor/{id}`
- **Açıklama:** Belirtilen ID'ye sahip yarışmacıyı günceller.
- **İstek Gövdesi:**

    ```json
    {
        "firstName": "Yeni İsim",
        "lastName": "Yeni Soyisim",
        "categoryId": 2
    }
    ```

- **Başarılı Yanıt:**
    - **Durum Kodu:** 200 OK
    - **Yanıt Gövdesi:**

        ```json
        {
            "id": 1,
            "firstName": "Yeni İsim",
            "lastName": "Yeni Soyisim",
            "categoryId": 2,
            "createDate": "2024-01-01T00:00:00Z",
            "modifiedDate": "2024-10-13T12:40:00Z",
            "isDeleted": false
        }
        ```

#### Yarışmacı Silme (Soft Delete)

- **Endpoint:** `DELETE /api/competitor/{id}`
- **Açıklama:** Belirtilen ID'ye sahip yarışmacıyı siler (soft delete).
- **Başarılı Yanıt:**
    - **Durum Kodu:** 200 OK
    - **Yanıt Gövdesi:**

        ```json
        {
            "id": 1,
            "firstName": "Ali",
            "lastName": "Veli",
            "categoryId": 1,
            "createDate": "2024-10-13T12:35:00Z",
            "modifiedDate": "2024-10-13T12:45:00Z",
            "isDeleted": true
        }
        ```

## API Dökümantasyonu

API'nizin tüm endpoint'lerini ve detaylarını incelemek için [Swagger UI](https://localhost:5001/swagger) kullanabilirsiniz. Swagger, API'nizin etkileşimli bir dökümantasyonunu sağlar ve endpoint'leri doğrudan test etmenize olanak tanır.

![Swagger UI](https://i.imgur.com/your-image-url.png) <!-- Kendi Swagger UI ekran görüntünüzü ekleyebilirsiniz -->


