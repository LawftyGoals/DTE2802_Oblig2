import globals from "globals";
import pluginJs from "@eslint/js";
import eslintConfigJquery from "@eslint-config-jquery";

export default [
  { languageOptions: { globals: globals.browser } },
  pluginJs.configs.recommended,
  {
    plugins: { jquery: eslintConfigJquery },
    extends: "jquery",
  },
];
