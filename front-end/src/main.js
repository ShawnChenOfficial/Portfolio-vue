import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import "../node_modules/bootstrap/dist/css/bootstrap.min.css";
import "../node_modules/jquery/dist/jquery.min.js";
import "../node_modules/bootstrap/dist/js/bootstrap.bundle.min.js";
import "./assets/css/site.css";
import axios from "axios";
import VueAnalytics from "vue-analytics";

axios.defaults.headers.get["Content-Type"] = "application/json;charset=UTF-8";
axios.defaults.headers.post["Content-Type"] = "application/json;charset=UTF-8";

Vue.config.productionTip = false;

Vue.use(VueAnalytics, {
  id: "Google-Analytics-ID",
  router
});

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
