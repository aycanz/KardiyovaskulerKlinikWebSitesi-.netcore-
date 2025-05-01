function getResults() {
  let kimlik = document.getElementById("kimlik").value;
  if (kimlik === "") {
      alert("Lütfen kimlik numaranızı girin!");
      return;
  }
  document.getElementById("sonuc").innerText = "Sonuçlarınız inceleniyor...";
  setTimeout(() => {
      document.getElementById("sonuc").innerText = "Test sonuçlarınız temiz.";
  }, 2000);
}

function analyzeSymptoms() {
  let belirtiler = document.getElementById("belirtiler").value;
  if (belirtiler === "") {
      alert("Lütfen belirtilerinizi girin!");
      return;
  }
  document.getElementById("tespitSonucu").innerText = "Analiz ediliyor...";
  setTimeout(() => {
      document.getElementById("tespitSonucu").innerText = "Olası hastalık: Hipertansiyon";
  }, 2000);
}
