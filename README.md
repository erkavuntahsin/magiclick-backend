# magiclick-backend

Bu repository, frontend'ine çok ağırlık verilmemiş bir **basic backend** projesi barındırır ve md uzantılı olacak şekilde js ve css örnek kodları mevcuttur.

Proje **.net core** tabanlı olup **n-tier** katmanlı mimariye sahiptir. Basic olarak backend yeteneklerimi göstermek için hazırlamış olduğum bir projedir.

### NOTLAR :
Bu proje kodları yaklaşık **iki** yılı aşkın bir süre önce yazılmıştır.
-- Owin ile login ve role management yapmayı genel olarak sevmiyorum kendim yazmayı daha çok seviyorum.
genel olarak kendim de yazıyor olsam aspect oriented mantığından kesinlikle faydalanmayı tercih ediyorum.

Proje genel olarak;
### MVC projesi

| Api| İşlem | Kullanım | 
|--|--|--|
| Login | Verilen bilgiler ile OWIN üzerinden Login işlemi yapar. | Username ve Password gerekir. |  
| Forgot Password | OWIN sistemi üzerinden parolayı mail atar. | - |
| Product | Get-Post-Put-Delete işlemleri yapılır. (Http türüne göre) | - |
| ProductDetail | Get işlemi yapar. | ProductId bilgisi gerekir. | 

 - Ekranlar
   * Login
   * Şifremi unuttum
   * Kullanıcı adını hatırla (cookie)
   * kategori sayfası
   * Ürün Listesi
   * Ürün gösterme
   

* ORM tarafında **Entity Framework** kullanılıldı
* Veritabanı tarafında **mssql** kullanıldı.
* Login ve role için OWIN Security(Identity) tabloları kullanıldı.
* Ürün sayfasına sadece ***product_view*** yetkisi olanlar girebiliyor.
* Kategori sayfası recursive yapıdadır db den sağlanmıştır. (*Basic bir yapıdadır.*)
* Proje üzerinde **JWT** kullanılmıştır.
* Proje içerisinde **Memory Cache** yapısı bulunmaktadır. (*Basic bir yapıdadır.*)
* Sql de hazırlanmış iki farklı stored procedure vardır. (*Semboliktir*.)
   * Kategorili ürün listesi (joinli)
   * Recursive kategori listesi
