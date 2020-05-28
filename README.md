# magiclick-backend
basic backend codes

proje .net core tabanlı olup n-tier katmanlı mimariye sahiptir. basic olarak backend yeteneklerimi göstermek için
hazırlamış olduğum bir proje.
NOTLAR :
*1 yılı aşkın bir süre önce yazılmıştır.
* Owin ile login ve role management yapmayı genel olarak sevmiyorum kendim yazmayı daha çok seviyorum.
genel olarak kendim de yazıyor olsam aspect oriented mantığından kesinlikle faydalanmayı tercih ediyorum.


Proje genel olarak;
1) MVC projesi

Ekranlar=>

* Login
* Şifremi unuttum
* Kullanıcı adını hatırla (cookie)
* kategori sayfası
* Ürün Listesi
* Ürün gösterme

2) MVC 

=> Login api / username password OWIN üzerinden login eder.
=> Şifremi unuttum => OWIN üzerinden şifreyi email atar.
=> Ürün listesi sayfası ve kategori sayfası için
 *  HttpGet => Ürün getir
 *  HttpPost => Ürün Ekle
 *  HttpPut => Ürün Güncelle
 *  HttpDelete => Ürün Sil

=> Ürün detay sayfası için 
* HttpGet /Id olarak kullanılmıştır.



* Framework tarafında EF kullanılıldı
* Db tarafında mssql kullanıldı.
* Login ve role için OWIN Security(Identity) tabloları kullanıldı ( yaklaşık 1 yıl önce yazdığım bir örnek projeydi)
* Ürün sayfasına sadece product_view yetkisi olanlar girebiliyor.
*Ürün tablosunda, id,name,catid,imageurl,price,isactive ve description alanları yeterlidir
* Kategori sayfası recursive yapıdadır db den sağlanmıştır. ancak basic olarak tasarlanmıştır.
* Webapi tarafı için JWT bearer tokeni kullanıldı.
*Memory Cache kullanıldı
*DI kullanıldı

3) SQL

Sql de hazırlanmış 2 farklı sp vardır.

1) Kategorili ürün listesi(joinli)
2) Recursive kategori listesi
