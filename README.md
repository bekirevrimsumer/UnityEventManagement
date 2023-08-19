# Unity'de Event Management

## TR

Bu kod, bir Unity projesinde Event'leri yönetmeyi ve bu Event'lere listener eklemeyi ve kaldırmayı sağlayan bir **Event Management** uygulamasıdır. EventManager, uygulamanızın farklı bileşenleri arasında iletişim kurmanıza yardımcı olan bir mekanizma sağlar.

## Neden Gereklidir ve Hangi Alanlarda İşleri Kolaylaştırır:

- **Bileşenler Arası İletişim**: Unity projelerinde farklı bileşenler arasında iletişim gerekebilir. Örneğin, bir oyun karakterinin ölümü gibi olaylar oluştuğunda diğer bileşenlerin buna tepki vermesi gerekebilir. EventManager, bu tür durumları kolayca yönetmeyi sağlar.

- **Azaltılmış Bağımlılık**: EventManager sayesinde bileşenler birbirine direkt bağlı olmadan iletişim kurabilir. Bu, bileşenlerin birbirlerine olan bağımlılığını azaltır ve yeniden kullanılabilirliği artırır.

- **Dinamik ve Esnek**: Eventler, dinamik bir şekilde eklenip çıkarılabilir. Böylece oyununuzun ilerleyen aşamalarında yeni özellikler eklemek veya var olanları değiştirmek daha kolay olur.

## Özellikler

- Event'leri dinlemek, tetiklemek ve kaldırmak için kolay kullanım sağlar.
- Event'lere öncelik ekleyerek dinleyicilerin sıralamasını ayarlama olanağı sunar.
- Özelleştirilebilir veri taşıma yeteneği sayesinde eventlere veri eklemeyi sağlar.

## Nasıl Kullanılır

- Event Oluşturma
```csharp
EventManager.AddListener("PlayerDeath", OnPlayerDeath);
EventManager.AddListener("UIPlayerDeath", OpenPlayerDeathPanel);
```
- Event Tetikleme
```csharp
EventManager.TriggerEvent("PlayerDeath");
EventManager.TriggerEvent("OpenPlayerDeathPanel");
```

- Event Kaldırma
```csharp
EventManager.RemoveListener("PlayerDeath", OnPlayerDeath);
EventManager.RemoveListener("UIPlayerDeath", OpenPlayerDeathPanel);
```

Etkinliğe Veri Eklemek ve Dinlemek:
```csharp
EventManager.AddListener("ScoreUpdated", OnScoreUpdated);
EventManager.TriggerEvent("ScoreUpdated", new EventData().AddData("Score", 100));
```

- Eklenen Veriyi Alma
```csharp
private void OnScoreUpdated(EventData eventData)
{
    int score = eventData.GetData<int>("Score");
}
```

- Event Önceliği
```csharp
EventManager.AddListener("PlayerDeath", OnPlayerDeath, 2); // Önceliği daha büyük olan eventler önce çalışır.
EventManager.AddListener("PlayerDeath", UIPlayerDeathMenu, 1);
```
