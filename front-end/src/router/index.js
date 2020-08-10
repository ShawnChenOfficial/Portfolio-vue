import Vue from "vue";
import VueRouter from "vue-router";
import AboutMe from "../views/AboutMe.vue";
import Resume from "../views/Resume.vue";
import Projects from "../views/Projects.vue";
import Contact from "../views/Contact.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "AboutMe",
    component: AboutMe
  },
  {
    path: "/AboutMe",
    name: "AboutMe",
    component: AboutMe
  },
  {
    path: "/Resume",
    name: "Resume",
    component: Resume
  },
  {
    path: "/Projects",
    name: "Projects",
    component: Projects
  },
  {
    path: "/Contact",
    name: "Contact",
    component: Contact
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
  scrollBehavior(to, from, saveTop) {
    if (saveTop) {
      return saveTop;
    } else {
      return { x: 0, y: 0 };
    }
  }
});

export default router;
