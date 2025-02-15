## Intro in Recoil

````
import React from "react";
import { atom, selector, RecoilRoot, useRecoilState, useRecoilValue } from "recoil";

// ✅ Atom für den Zustand
const counterState = atom<number>({
  key: "counterState",
  default: 0,
});

// ✅ Selector für einen berechneten Wert
const doubledCounterState = selector<number>({
  key: "doubledCounterState",
  get: ({ get }) => get(counterState) * 2,
});

// ✅ Komponente mit Recoil-Zustand
const Counter: React.FC = () => {
  const [count, setCount] = useRecoilState(counterState);
  const doubled = useRecoilValue(doubledCounterState);

  return (
    <div>
      <p>Count: {count}</p>
      <p>Doppelter Wert: {doubled}</p>
      <button onClick={() => setCount(count + 1)}>+</button>
      <button onClick={() => setCount(count - 1)}>-</button>
    </div>
  );
};

// ✅ App mit RecoilRoot
const App: React.FC = () => (
  <RecoilRoot>
    <Counter />
  </RecoilRoot>
);

export default App;
````

## ThemeSwitcher

````
// ✅ Atom für den Zustand
// recoil/themeAtom.ts
import { atom } from "recoil";

export type Theme = "light" | "dark";

export const themeState = atom<Theme>({
  key: "themeState",
  default: "light",
});

// ✅ Komponente mit Recoil-Zustand  components/ThemeSwitcher.tsx
import React from "react";
import { useRecoilState } from "recoil";
import { themeState, Theme } from "../recoil/themeAtom";

const ThemeSwitcher: React.FC = () => {
  const [theme, setTheme] = useRecoilState(themeState);

  const toggleTheme = () => {
    setTheme((prev: Theme) => (prev === "light" ? "dark" : "light"));
  };

  return (
    <button onClick={toggleTheme}>
      Wechsel zu {theme === "light" ? "Dark Mode" : "Light Mode"}
    </button>
  );
};

export default ThemeSwitcher;

// ✅ App mit RecoilRoot
// App.tsx
import React from "react";
import { RecoilRoot, useRecoilValue } from "recoil";
import { themeState } from "./recoil/themeAtom";
import ThemeSwitcher from "./components/ThemeSwitcher";

const ThemedApp: React.FC = () => {
  const theme = useRecoilValue(themeState);

  return (
    <div style={{
      backgroundColor: theme === "light" ? "#fff" : "#333",
      color: theme === "light" ? "#000" : "#fff",
      height: "100vh",
      display: "flex",
      alignItems: "center",
      justifyContent: "center",
      flexDirection: "column"
    }}>
      <h1>{theme === "light" ? "Light Mode" : "Dark Mode"}</h1>
      <ThemeSwitcher />
    </div>
  );
};

const App: React.FC = () => (
  <RecoilRoot>
    <ThemedApp />
  </RecoilRoot>
);

export default App;
````