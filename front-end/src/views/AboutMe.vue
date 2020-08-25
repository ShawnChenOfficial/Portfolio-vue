<template>
  <div id="myProtfilio" class="container-fluid p-0">
    <div class="text-center pt-20vh" id="first-Page-20vh">
      <div class="container">
        <div class="pb-5 profile-picture"></div>
        <h1 class="text-white text-huge pt-4">Shawn Chen</h1>
        <p class="pt-3 w-75 m-auto">
          I'm in the final year of a Bachelor of Information Technology at the
          Southern Institute of Technology in Invercargill. I am a programmer
          who loves programming and I keep
          <strong class="text-white">researching</strong> and
          <strong class="text-white">learning</strong> new skills related to IT.
        </p>
        <div class="text-center border-div-trigger w-80px m-auto pt-2">
          <router-link class="text-decoration-none" to="/Contact"
            >Email Me</router-link
          >
          <div class="border-div"></div>
        </div>
      </div>
    </div>
    <div class="text-center" id="second-Page">
      <div class="container vertical-middle pt-v-sm-20vh">
        <h3>I am good at</h3>
        <h1 class="text-white">Web application development.</h1>
        <p class="pt-4 w-75 m-auto">
          Open mindset for cutting edge web technologies makes for a full-stack
          developer with huge potential. Team player with strong communication
          skills can fit into dynamic development team. Also, an organised
          independent worker with strong time management skills.
        </p>
      </div>
    </div>
    <div class="text-center pb-5" id="third-Page">
      <div class="container vertical-middle pt-v-sm-20vh">
        <h1 class="pb-5 text-white">More About Me</h1>
        <div class="row pt-5">
          <div class="col-sm-8 offset-sm-2 offset-md-1 col-md-4 mb-4">
            <div class="card bg-none">
              <span
                class="material-icons card-img-top pt-4 font-88px text-wheat"
                >description</span
              >
              <div class="card-body">
                <h5 class="card-title text-white">Resume</h5>
                <p class="card-text">
                  To get general idea of my education background, my skills
                  learnt so far, acheivements and so on...
                </p>
                <div class="text-center border-div-trigger w-80px m-auto">
                  <router-link class="text-decoration-none" to="/Resume"
                    >See more</router-link
                  >
                  <div class="border-div"></div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-sm-8 offset-sm-2 offset-md-2 col-md-4 mb-4">
            <div class="card bg-none">
              <span
                class="material-icons card-img-top pt-4 font-88px text-wheat"
                >file_copy</span
              >
              <div class="card-body">
                <h5 class="card-title text-white">Projects</h5>
                <p class="card-text">
                  To understand the level of my skills by diagrams, source codes
                  or a link to a live system.
                </p>
                <div class="text-center border-div-trigger w-80px m-auto">
                  <router-link class="text-decoration-none" to="/Projects"
                    >See more</router-link
                  >
                  <div class="border-div"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div id="Top" @click="Top">
      <span class="material-icons">
        arrow_upward
      </span>
      <p class="pl-1 mb-0">
        Top
      </p>
    </div>
    <div @click="Next" id="furtherInfo">
      <p class="mb-0">
        Next
      </p>
      <span class="material-icons">
        arrow_downward
      </span>
    </div>
  </div>
</template>

<script>
import $ from "jquery";
import root from "../App.vue";
// @ is an alias to /src
export default {
  name: "AboutMe",
  mounted() {
    $(window).unbind("scroll");
    $(window).on("load", root.methods.UpDownArrowsSwitcher());
    $(window).resize(function() {
      root.methods.UpDownArrowsSwitcher();
    });
    $(window).scroll(function() {
      var current = $(document).scrollTop();
      var firstP = $("#first-Page-20vh").offset().top;
      var secondP = $("#second-Page").offset().top;

      if (!root.methods.ToDisableNavigationArrows()) {
        if (current <= firstP + 10) {
          $("#furtherInfo").css({ display: "block" });
        } else if (current > secondP - 40 && current <= secondP + 100) {
          $("#furtherInfo").css({ display: "block" });
        } else {
          $("#furtherInfo").css({ display: "none" });
        }
      }
    });
  },
  methods: {
    Next: function() {
      var current = $(document).scrollTop();
      var firstP = $("#first-Page-20vh").offset().top;
      var secondP = $("#second-Page").offset().top;

      if (current <= firstP + 10) {
        $("html, body").animate({ scrollTop: secondP + 100 }, 1000);
        $("html, body").animate({ scrollTop: secondP - 60 }, 500);
        $("html, body").animate({ scrollTop: secondP + 20 }, 400);
        $("html, body").animate({ scrollTop: secondP }, 300);
      } else if (current > secondP - 40 && current <= secondP + 100) {
        $("html, body").animate(
          { scrollTop: $("#third-Page").offset().top + 100 },
          500
        );
      }
    },
    Top: function() {
      root.methods.Top();
    }
  }
};
</script>
