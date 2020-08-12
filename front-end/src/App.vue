<template>
  <div id="app">
    <div id="views">
      <div v-bind:class="ViewersClass">
        <h5 class="pr-1 text-white mb-0 views-num-top">
          {{ ViewersTopValue }}
        </h5>
        <h5 class="pr-1 text-white mb-0 views-num-bottom">
          {{ ViewersBottomValue }}
        </h5>
        <h5 class="pr-1 text-white mb-0 views-num-extender">
          {{ ViewersHideValue }}
        </h5>
      </div>
      <p class="mb-0 pr-2">viewers</p>
    </div>
    <nav
      class="navbar fixed-top navbar-expand-sm navbar-toggleable-sm navbar-dark bg-black box-shadow"
    >
      <div class="container">
        <a
          class="navbar-brand"
          asp-area=""
          asp-controller="Home"
          asp-action="AboutMe"
          >Shawn Chen</a
        >
        <button
          class="navbar-toggler"
          type="button"
          data-toggle="collapse"
          data-target=".navbar-collapse"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <div
          class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse"
        >
          <ul
            class="navbar-nav flex-grow-1 nav-ul-absolute justify-content-center"
          >
            <li class="nav-item">
              <router-link class="nav-link text-center text-light" to="/AboutMe"
                >About Me</router-link
              >
              <div class="border-div"></div>
            </li>
            <li class="nav-item">
              <router-link class="nav-link text-center text-light" to="/Resume"
                >Resume</router-link
              >
              <div class="border-div"></div>
            </li>
            <li class="nav-item">
              <router-link
                class="nav-link text-center text-light"
                to="/Projects"
                >Projects</router-link
              >
              <div class="border-div"></div>
            </li>
            <li class="nav-item">
              <router-link class="nav-link text-center text-light" to="/Contact"
                >Contact</router-link
              >
              <div class="border-div"></div>
            </li>
          </ul>
        </div>
      </div>
    </nav>
    <router-view />
  </div>
</template>

<script>
import $ from "jquery";
import axios from "axios";

export default {
  data: function() {
    return {
      url_new: this.$store.getters.backEnd_baseUrl + "/api/NewViewer",
      url_refresh: this.$store.getters.backEnd_baseUrl + "/api/GetViewers",
      ViewersTopValue: 0,
      ViewersBottomValue: 0,
      ViewersHideValue: 0,
      ViewersClass: "views-bottom-to-top"
    };
  },
  created() {
    axios.get(this.url_new).then(console.log("Welcome to Shawn's Website"));
    this.RefreshViewers();
  },
  mounted() {
    window.setInterval(this.RefreshViewers, 5000);
  },
  methods: {
    RefreshViewers: function() {
      axios
        .get(this.url_refresh)
        .then(response => {
          if (response.data != this.ViewersHideValue) {
            if (this.ViewersClass == "views-bottom-to-top") {
              this.ViewersBottomValue = response.data;
              this.ViewersClass = "views-top-to-bottom";
            } else if (this.ViewersClass == "views-top-to-bottom") {
              this.ViewersTopValue = response.data;
              this.ViewersClass = "views-bottom-to-top";
            }
            this.ViewersHideValue = response.data;
          }
        })
        .catch(error => {
          console.log(error);
          if (this.ViewersClass == "views-bottom-to-top") {
            this.ViewersBottomValue = 0;
            this.ViewersClass = "views-top-to-bottom";
          } else if (this.ViewersClass == "views-top-to-bottom") {
            this.ViewersTopValue = 0;
            this.ViewersClass = "views-bottom-to-top";
          }
        });
    },
    ToDisableNavigationArrows: function() {
      return (
        $(window).height() < 768 ||
        $(window).width() < 768 ||
        ($(window).width() < 768 &&
          $("#Contact")
            .parent()
            .find("div")
            .hasClass("border-div-active"))
      );
    },
    Top: function() {
      $("html, body").animate({ scrollTop: 0 }, 1000);
    },
    UpDownArrowsSwitcher: function() {
      if (this.ToDisableNavigationArrows()) {
        $("#Top").css({ display: "none" });
        $("#furtherInfo").css({ display: "none" });
      } else {
        $("#Top").css({ display: "block" });
        $("#furtherInfo").css({ display: "block" });
        window.scroll(0, $(document).scrollTop() + 1);
        window.scroll(0, $(document).scrollTop() - 1);
      }
    }
  }
};
</script>
