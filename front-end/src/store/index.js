import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    frontEnd_baseUrl:
      process.env.NODE_ENV == "development"
        ? "http://localhost:8977"
        : "https://shawnchen.co.nz",
    frontEnd_port: 8977,
    backEnd_baseUrl:
      process.env.NODE_ENV == "development"
        ? "https://localhost:8976"
        : "https://shawnchen.co.nz",
    backEnd_port: 8976
  },
  getters: {
    frontEnd_baseUrl: state => {
      return state.frontEnd_baseUrl;
    },
    backEnd_baseUrl: state => {
      return state.backEnd_baseUrl;
    }
  },
  mutations: {},
  actions: {},
  modules: {}
});
