<template>
    <b-button :id="_id"
              :class="field.name"
              :title="field.caption"
              @click="listener(section,field)"
              variant="outline-primary">
        <!--<div @contextmenu.prevent="$refs.contextMenu.open(section,arguments[0],viewModel[field.name])" class="d-inline-block">-->
            <!--<div @contextmenu.prevent="$refs.contextMenu.open(section,arguments[0])" class="d-inline-block">-->
                <span v-html="field.caption"></span>
            <!--</div>-->
</b-button>
</template>

<script>
    /* ******************************************************************
    *
    * ae-tag-xxxxxxxx
    *
    * in  string    page
    * in  string    section
    * in  {}        field
    * in  {}        viewModel
    * in  {}        state
    *
    ******************************************************************* */
    import aeTag from "./ae-tag.js"
    export default {
        mixins: [aeTag],
        props: [
        ],
        data() {
            return {
            }
        },
        watch: {

        },
        methods: {

            /** *****************************************************************
            *
            * コンテキストメニュー
            *
            ******************************************************************* */
            onContextMenu(action, id) {
                if (action === "edit") {

                    alert(id)
                }
            },
        },
 
    }

</script>
<style scoped>
</style>
