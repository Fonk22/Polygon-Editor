# 🎨 Polygon Editor

Polygon Editor to aplikacja graficzna stworzona w Windows Forms.
Program umożliwia intuicyjne tworzenie i edycję wielokątów, a także wprowadzanie precyzyjnych ograniczeń geometrycznych.
Dzięki niemu użytkownik może swobodnie modelować kształty, stosując zarówno klasyczne odcinki, jak i segmenty krzywych Béziera.

## ✨ Kluczowe funkcje
- **Tworzenie i edycja wielokątów**:
  - Intuicyjne tworzenie wielokątów poprzez dodawanie i usuwanie wierzchołków oraz krawędzi
  - Wygodne przesuwanie wierzchołków oraz całego wielokąta
  - Ustawianie ograniczeń dla krawędzi oraz wierzchołków
  - Zapis i wczytywanie wielokątów, umożliwiające przechowywanie projektu w pliku i jego późniejszą edycję.
- **Obsługa krzywych Béziera 3-go stopnia**:
  - Zamiana krawędzi na segmenty Béziera
  - Manipulacja punktami kontrolnymi
  - Opcja ustawienia klasy ciągłości w wierzchołku 
- **Zaawansowane ograniczenia geometryczne**:
  - Ograniczenia dla krawędzi wielokąta:
    - Ustalona długość krawędzi (zachowuje stałą długość podczas edycji)
    - Krawędź pionowa – krawędź zawsze prostopadła do osi X
    - Krawędź pozioma – krawędź zawsze równoległa do osi X
  - Ciągłość klasy C1 w wierzchołku - ciągłość wektora stycznego
  - Ciągłość klasy G1 w wierzchołku - ciągłość jednostkowego wektora stycznego
- **Precyzyjne rysowanie**:
  - Wykorzystanie algorytmu Bresenhama do rysowania linii
  - Algorytm przyrostowy do renderowania krzywych Béziera
