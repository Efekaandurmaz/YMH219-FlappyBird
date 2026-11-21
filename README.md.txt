# YMH219-FlappyBird

Windows Forms ile geliştirilmiş Flappy Bird benzeri masaüstü oyunu. Nesne Tabanlı Programlama (YMH 219) dersi uygulama ödevidir.

**Öğrenci:** [EFE KAAN DURMAZ] - [250541106]

## Kullanılan Teknolojiler
- C#
- .NET 10 (Windows Forms)
- Visual Studio 2026
- Harici NuGet paketi yok

## Kurulum ve Çalıştırma
1. Depoyu klonlayın: `git clone https://github.com/[efekaandurmaz]/YMH219-FlappyBird.git`
2. `.sln` dosyasını Visual Studio ile açın.
3. **F5** ile çalıştırın.

## Oynanış
- **Space** veya **↑**: Başlatır ve kuşu zıplatır
- **R** veya **Yeni Oyun** butonu: Yeniden başlatır

## Temel Özellikler
- Timer ile oyun döngüsü
- Yer çekimi ve zıplama
- Sağdan sola hareket eden 3 engel, ekran dışına çıkınca yeniden konumlanır
- Engel veya zemine çarpınca Game Over
- Engel geçildikçe skor artışı
- Seviye arttıkça hız artışı
- Yeni oyun / yeniden başlatma

## Sınıf Yapısı
- `Bird`: Oyuncunun konumu, hızı ve zıplaması
- `Pipe`: Üst ve alt borudan oluşan engel
- `Game`: Oyun kuralları, skor, seviye, çarpışma
- `GameForm`: Arayüz, Timer, klavye olayları ve çizim

## Ekran Görüntüleri
![Oyun](screenshots/oyun.png)
![Game Over](screenshots/gameover.png)

## Bilinen Sınırlamalar
- Yüksek skor kaydedilmez.
- Ses efekti yoktur.